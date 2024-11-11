using EzTabs.Data.Repository;
using EzTabs.Model;
using EzTabs.Services.ModelServices.BaseServices;
using EzTabs.Services.RepoServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.Services.ModelServices
{
    public class TabService : BaseService
    {
        private RepoImplementation<Tab>? _tabRepository;
        public static Tab? SavedTab { get; private set; }

        public TabService()
        {
            _initializeTask = Task.Run(async () =>
            {
                _tabRepository = await InitializeRepoAsync<Tab>();
            });
        }


        public async Task<bool> CreateTab(Guid authorId, string title, string band, string genre, string key, int bpm, string description, List<Tuning> tunings)
        {
            await EnsureRepositoryInitialized();

            if (_tabRepository is null) throw new ArgumentNullException(nameof(_tabRepository) + "Haven't loaded in time");
            var allTabs = await _tabRepository.GetAll();
            if (allTabs.FirstOrDefault(t => t.AuthorId == authorId && t.Title == title && t.Band == band) != null) return false;
            var newTab = new Tab
            {
                AuthorId = authorId,
                Title = title,
                Band = band,
                Genre = genre,
                Key = key,
                BitsPerMinute = bpm,
                Description = description
            };


            await _tabRepository.Add(newTab);
            return true;
        }
    }
}
