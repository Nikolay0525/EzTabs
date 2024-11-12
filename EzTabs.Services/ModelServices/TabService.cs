using EzTabs.Data.Repository;
using EzTabs.Model;
using EzTabs.Services.ModelServices.BaseServices;

namespace EzTabs.Services.ModelServices
{
    public class TabService : BaseService<Tab>
    {
        public static Tab? SavedTab { get; private set; }

        public TabService()
        {
            _initializeTask = Task.Run(InitializeRepoAsync);
        }

        public async Task<Tab?> CreateTab(Guid authorId, string title, string band, string genre, string key, int bpm, string description)
        {
            await EnsureRepositoryInitialized();

            if (_repository is null) throw new ArgumentNullException(nameof(_repository) + "Haven't loaded in time");
            var allTabs = await _repository.GetAll();
            if (allTabs.FirstOrDefault(t => t.AuthorId == authorId && t.Title == title && t.Band == band && t.Genre == genre) != null) return null;
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
            await _repository.Add(newTab);
            SavedTab = newTab;
            return newTab;
        }
    }
}
