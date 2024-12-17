using EzTabs.Data;
using EzTabs.Data.Domain;

using EzTabs.Presentation.Services.DomainServices.BaseServices;
using EzTabs.Presentation.Services.NavigationServices;

namespace EzTabs.Presentation.Services.DomainServices;

public class TuningService : BaseService<Tuning>
{
    public TuningService(EzTabsContext context, INavigationService navigationService) : base(context, navigationService)
    {
        
    }

    public async Task CreateTuning(Tab tab, List<Tuning> tunings)
    {
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

        var operation = await _repository.Add(tunings);
        if (!operation.Success) throw new InvalidOperationException(operation.ErrorMessage);
    }
    
    public async Task<List<Tuning>> GetAllTunings(Tab tab)
    {
        var alltunings = await _repository.GetAll();
        if (!alltunings.Success) throw new InvalidOperationException(alltunings.ErrorMessage);

        List<Tuning> tunings = new List<Tuning>();
        foreach(var tuning in alltunings.Data!)
        {
            if (tuning.TabId == tab.Id) tunings.Add(tuning);
        }
        return tunings;
    }
}
