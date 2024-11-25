using EzTabs.Data;
using EzTabs.Data.Domain;
using EzTabs.Presentation.Services.ContextServices;
using EzTabs.Presentation.Services.DomainServices.BaseServices;
using EzTabs.Presentation.Services.NavigationServices;

namespace EzTabs.Presentation.Services.DomainServices;

public class TuningService : BaseService<Tuning>
{
    public TuningService(IContextFactoryService contextFactoryService, INavigationService navigationService) : base(contextFactoryService, navigationService)
    {
        
    }

    public async Task CreateTuning(Tab tab, List<Tuning> tunings)
    {
        await EnsureRepoCreated();

        List<Tuning> emptyLines = new()
        {
            new Tuning { StringNote = string.Empty, StringOrder = -1},
            new Tuning { StringNote = string.Empty, StringOrder = 0}
        };

        tunings.AddRange(emptyLines);

        foreach (var tuning in tunings)
        {
            tuning.TabId = tab.Id;
        }

        tunings.Add(new Tuning() { StringNote = string.Empty, StringOrder = tunings.Count+1, TabId = tab.Id });

        await _repository.Add(tunings);
    }
    
    public async Task<List<Tuning>> GetAllTunings(Tab tab)
    {
        await EnsureRepoCreated();

        IEnumerable<Tuning> alltunings = await _repository.GetAll();
        List<Tuning> tunings = new List<Tuning>();
        foreach(var tuning in alltunings)
        {
            if (tuning.TabId == tab.Id) tunings.Add(tuning);
        }
        return tunings;
    }
}
