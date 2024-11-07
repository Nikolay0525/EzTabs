using NUnit.Framework;
using EzTabs.Model;
using EzTabs.Services.ModelServices;
using EzTabs.Data.Repository;
using EzTabs.Services.RepoServices;
using EzTabs.Services.EmailServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace EzTabs.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService _userService;
        private RepoImplementation<User> _userRepository;

        [SetUp]
        public void SetUp()
        {
            _userService = new UserService();
            _userRepository = RepoImplementation<User>.CreateRepoSync();
        }

        [Test]
        public async Task RegisterUser_ValidUser_ReturnsNoErrors()
        {

            // Arrange
            var name = "exactlynewUser";
            var email = "newuser@email.com";
            var password = "securePassword";
            var verificationCode = "12345";

            // Act
            var result = await _userService.RegisterUser(name, email, password, verificationCode);


            // Assert
            Assert.That(result, Is.Empty); // No errors expected
        }

        [Test]
        public async Task RegisterUser_UsernameAlreadyTaken_ReturnsError()
        {
            // Arrange
            var existingUser = new User { Name = "existingUser", Email = "existing@email.com", Password = "password", VerificationCode = "12345" };
            await _userRepository.Add(existingUser);

            var name = "existingUser";
            var email = "newuser@email.com";
            var password = "securePassword";
            var verificationCode = "12345";

            // Act
            var result = await _userService.RegisterUser(name, email, password, verificationCode);

            // Assert
            Assert.That(result, Does.Contain("Username: Such name already used"));
        }

        [Test]
        public async Task RegisterUser_EmailAlreadyTaken_ReturnsError()
        {
            // Arrange
            var existingUser = new User { Name = "newUser", Email = "existing@email.com", Password = "password", VerificationCode = "12345" };
            await _userRepository.Add(existingUser);

            var name = "newUser";
            var email = "existing@email.com";
            var password = "securePassword";
            var verificationCode = "12345";

            // Act
            var result = await _userService.RegisterUser(name, email, password, verificationCode);

            // Assert
            Assert.That(result, Does.Contain("Email: Such email already used"));
        }

        [Test]
        public async Task LoginUser_ValidCredentials_ReturnsTrue()
        {
            // Arrange
            var userToLogin = new User
            {
                Name = "validUser",
                Password = "password"
            };
            await _userRepository.Add(userToLogin);

            // Act
            var result = await _userService.LoginUser(userToLogin);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(UserService.SavedUser.Name, Is.EqualTo("validUser")); // Ensure the user is saved as the logged-in user
        }

        [Test]
        public async Task LoginUser_InvalidCredentials_ReturnsFalse()
        {
            // Arrange
            var validUser = new User
            {
                Name = "validUser",
                Password = "password"
            };
            await _userRepository.Add(validUser);

            var userToLogin = new User
            {
                Name = "validUser",
                Password = "wrongPassword"
            };

            // Act
            var result = await _userService.LoginUser(userToLogin);

            // Assert
            Assert.That(result, Is.False);
            Assert.That(UserService.SavedUser.Name, Is.Not.EqualTo("validUser")); // Ensure the logged-in user is not set
        }
    }
}
