﻿using EzTabs.Data;
using EzTabs.Data.Domain;
using EzTabs.Data.Domain.Enums;

using EzTabs.Presentation.Services.DomainServices.BaseServices;
using EzTabs.Presentation.Services.EmailServices;
using EzTabs.Presentation.Services.NavigationServices;

namespace EzTabs.Presentation.Services.DomainServices;

public class UserService : BaseService<User>
{
    public static User SavedUser { get; private set; } = new User();

    public UserService(EzTabsContext context, INavigationService navigationService) : base(context, navigationService)
    {

    }

    public async Task<List<string>> RegisterUser(string name, string email, string password, string verificationCode)
    {
        List<string> errors = new();
        User newUser = new()
        {
            Name = name,
            Email = email,
            Password = password,
            VerificationCode = verificationCode
        };
        if (name is null || email is null || password is null) throw new NullReferenceException("Some of user data is missing");
        var users = await _repository.GetAll();
        if (users.FirstOrDefault(u => u.Name == newUser.Name) != null)
        {
            errors.Add("Username: Such name already used");
        }
        if (users.FirstOrDefault(u => u.Email == newUser.Email) != null)
        {
            errors.Add("Email: Such email already used");
        }
        if (errors.Count != 0) return errors;
        newUser.Role = users.Any() ? UserRole.User : UserRole.Admin;
        await _repository.Add(newUser);
        SavedUser = newUser;
        await Task.Run(() => EmailService.SendVerificationEmail(email, verificationCode));
        return errors;
    }

    public async Task<bool> VerificateUser(string verificationCode)
    {
        if (SavedUser == null || verificationCode == null)
        {
            throw new NullReferenceException();
        };
        if (SavedUser.VerificationCode == verificationCode)
        {
            SavedUser.IsEmailVerified = true;
            await _repository.Update(SavedUser);
            return true;
        }
        return false;
    }
    public async Task<bool> LoginUser(User userToLogin)
    {     
        var allUsers = await _repository.GetAll();
        var foundedUser = allUsers.FirstOrDefault(u => u.Name == userToLogin.Name);
        if (foundedUser != null && foundedUser.Password == userToLogin.Password)
        {
            SavedUser = foundedUser;
            return true;
        }
        return false;
    }

    public async Task<User?> FindUserById(Guid userId)
    {
        var foundedUser = await _repository.GetById(userId); return foundedUser;
    }
}
