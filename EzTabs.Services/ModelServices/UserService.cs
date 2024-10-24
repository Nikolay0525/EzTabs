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
        private RepoImplementation<User>? _userRepository;
        public static User? SavedUser { get; private set; } 

        public UserService()
        {
            Task.Run(InitializeAsync);
        }

        private async Task InitializeAsync()
        {
            _userRepository =  await RepoInitializeService.InitializeRepoAsync<User>();
        }

        public async Task<List<string>> RegisterUser(string name, string email, string password, string verificationCode)
        {
            if (_userRepository is null) throw new ArgumentNullException(nameof(_userRepository));
            List<string> errors = new();
            User newUser = new()
            {
                Name = name,
                Email = email,
                Password = password,
                VerificationCode = verificationCode
            };
            if (name is null || email is null || password is null) throw new NullReferenceException("Some of user data is missing");
            var users = await _userRepository.GetAll();
            if (users.FirstOrDefault(u => u.Name == newUser.Name) != null) 
            {
                errors.Add("Username: Such name already used");
            }
            if (users.FirstOrDefault(u => u.Email == newUser.Email) != null) 
            {
                errors.Add("Email: Such email already used");
            }
            if (errors.Count != 0) return errors;
            newUser.Role = users.Any() ? Model.Enums.UserRole.Admin : Model.Enums.UserRole.User;
            await _userRepository.Add(newUser);
            SavedUser = newUser;
            EmailService.SendVerificationEmail(email, verificationCode);
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
                await _userRepository.Update(SavedUser);
                SavedUser = null;
                return true;
            }
            return false;
        }
        public async Task<bool> LoginUser(User userToLogin)
        {
            var allUsers = await _userRepository.GetAll();
            var foundedUser = allUsers.FirstOrDefault(u => u.Name == userToLogin.Name);
            if (foundedUser != null && foundedUser.Password == userToLogin.Password)
            {
                SavedUser = foundedUser;
                return true;
            }
            return false;
        }
    }
}
