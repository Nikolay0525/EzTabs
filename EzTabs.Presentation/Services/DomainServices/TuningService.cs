using EzTabs.Data.Domain;
using EzTabs.Presentation.Services.DomainServices.BaseServices;

namespace EzTabs.Presentation.Services.DomainServices;

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
