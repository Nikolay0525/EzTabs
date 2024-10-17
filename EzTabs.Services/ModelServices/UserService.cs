using EzTabs.Services.EmailServices;
using EzTabs.Data.Repository;
using EzTabs.Model;
using EzTabs.Data;
using EzTabs.Services.RepoServices;
using EzTabs.Model.Enums;

namespace EzTabs.Services.ModelServices
{
    public class UserService
    {
        private RepoImplementation<User> _userRepository;
        private static User? _savedUser = null; 

        public UserService()
        {
            Task.Run(InitializeAsync);
        }

        public static User? GetSavedUser()
        {
            return _savedUser;
        }

        private async Task InitializeAsync()
        {
            _userRepository =  await RepoInitializeService.InitializeRepoAsync<User>();
        }

        public async Task RegisterUser(string name, string password, string email, string verificationCode)
        {
            UserRole role = UserRole.User;
            if (name is null || email is null || password is null) throw new NullReferenceException("Some of user data is missing");
            EmailService.SendVerificationEmail(email, verificationCode);
            var users = await _userRepository.GetAll();
            if (!users.Any()) role = Model.Enums.UserRole.Admin;
            User newUser = new()
            {
                Name = name,
                Email = email,
                Password = password,
                Role = role
            };
            await _userRepository.Add(newUser);
            _savedUser = newUser;
        }

        public async Task VerificateUser(string verificationCode)
        {
            if (_savedUser == null || verificationCode == null) 
            {
                throw new NullReferenceException();
            };
            if (_savedUser.VerificationCode == verificationCode)
            {
                _savedUser.IsEmailVerified = true;
                await _userRepository.Update(_savedUser);
                _savedUser = null;
            }
            return;
        }
        public async Task<bool> LoginUser(User userToLogin)
        {
            var allUsers = await _userRepository.GetAll();
            var foundedUser = allUsers.FirstOrDefault(u => u.Name == userToLogin.Name);
            if (foundedUser != null && foundedUser.Password == userToLogin.Password)
            {
                _savedUser = foundedUser;
                return true;
            }
            return false;
        }
    }
}
