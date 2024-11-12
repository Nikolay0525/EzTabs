using EzTabs.Data.Repository;
using EzTabs.Model;
using EzTabs.Services.ModelServices.BaseServices;

namespace EzTabs.Services.ModelServices
{
    public class TuningService : BaseService<Tuning>
    {
        public TuningService()
        {
            _initializeTask = Task.Run(InitializeRepoAsync);
        }

        public async Task CreateTuning(Tab tab, List<Tuning> tunings)
        {
            await EnsureRepositoryInitialized();
            if (_repository is null) throw new ArgumentNullException(nameof(_repository));

            foreach (var tuning in tunings)
            {
                tuning.TabId = tab.Id;
            }

            await _repository.Add(tunings);
        }
    }
}
