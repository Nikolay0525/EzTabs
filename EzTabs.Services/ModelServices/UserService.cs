using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzTabs.Model.Model;
using EzTabs.Data.Repository;

namespace EzTabs.Services.ModelServices
{
    public class UserService
    {
        private readonly RepoImplementation<User> _userRepository;
        public UserService(RepoImplementation<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterUser(User user)
        {
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
