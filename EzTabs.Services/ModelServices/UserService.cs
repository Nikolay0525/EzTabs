using EzTabs.Services.EmailServices;
using EzTabs.Data.Repository;
using EzTabs.Model;
using EzTabs.Data;

namespace EzTabs.Services.ModelServices
{
    public class UserService
    {
        private readonly RepoImplementation<User> _userRepository;

        public UserService()
        {
            _userRepository = new RepoImplementation<User>(new EzTabsContext());
        }

        public async Task VerificateUser(User user, string verificationCode)
        {
            if (user == null || verificationCode == null) 
            {
                throw new NullReferenceException();
            };
            if (user.VerificationCode == verificationCode)
            {
                user.IsEmailVerified = true;
                await _userRepository.Update(user);
            }
            return;
        }

        public async Task RegisterUser(User? user, string verificationCode)
        {
            if (user == null || user.Email == null) return;
            EmailService.SendVerificationEmail(user.Email, verificationCode);
            await _userRepository.Add(user);
        }

        public async Task<User?> LoginUser(User userToLogin)
        {
            var allUsers = await _userRepository.GetAll();
            var foundedUser = allUsers.FirstOrDefault(u => u.Name == userToLogin.Name);
            if (foundedUser != null && foundedUser.Password == userToLogin.Password)
            {
                return foundedUser;
            }
            return null;
        }
    }
}
