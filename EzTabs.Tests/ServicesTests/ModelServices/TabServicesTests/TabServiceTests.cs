using NUnit.Framework;
using EzTabs.Model;
using EzTabs.Services.ModelServices;
using System;
using System.Collections.Generic;
using EzTabs.Data.Repository;
using EzTabs.Services.RepoServices;

namespace EzTabs.Tests
{
    [TestFixture]
    public class TabServiceTests
    {
        private TabService _tabService;
        private RepoImplementation<Tab> _tabRepository;
        private RepoImplementation<User> _userRepository;

        [SetUp]
        public void SetUp()
        {
            _tabService = new TabService();
            _tabRepository = RepoImplementation<Tab>.CreateRepoSync();
            _userRepository = RepoImplementation<User>.CreateRepoSync();
        }

        [Test]
        public async Task CreateTab_ValidTab_ReturnsTrue()
        {
            var author = new User()
            {
                Name = "author",
                Password = "password",
                Email = "email@domen.com",
                IsEmailVerified = true,
            };

            await _userRepository.Add(author);
            // Arrange
            var title = "New Song";
            var band = "Band X";
            var genre = "Rock";
            var key = "C";
            var bpm = 120;
            var description = "A great new song!";
            var tunings = new List<Tuning>(); // No tunings for simplicity

            // Act
            var result = await _tabService.CreateTab(author.Id, title, band, genre, key, bpm, description, tunings);

            // Assert
            Assert.That(result, Is.True); // Checks if result is true
        }

        [Test]
        public async Task CreateTab_TabAlreadyExists_ReturnsFalse()
        {
            var author = new User()
            {
                Name = "author",
                Password = "password",
                Email = "email@domen.com",
                IsEmailVerified = true,
            };

            await _userRepository.Add(author);

            // Arrange
            var title = "Existing Song";
            var band = "Band Y";
            var genre = "Pop";
            var key = "D";
            var bpm = 100;
            var description = "An existing song!";
            var tunings = new List<Tuning>(); // No tunings for simplicity

            // First, add the existing tab to the repository
            var existingTab = new Tab
            {
                AuthorId = author.Id,
                Title = title,
                Band = band,
                Genre = genre,
                Key = key,
                BitsPerMinute = bpm,
                Description = description
            };
            await _tabRepository.Add(existingTab);

            // Act
            var result = await _tabService.CreateTab(author.Id, title, band, genre, key, bpm, description, tunings);

            // Assert
            Assert.That(result, Is.False); // Checks if result is false because the tab already exists
        }
    }
}