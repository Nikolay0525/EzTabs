using EzTabs.Data;
using EzTabs.Data.Domain;
using EzTabs.Data.Repository;

using EzTabs.Presentation.Services.DomainServices.BaseServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.ViewModels.MainControlsViewModels;
using System.Text.Json;
using System.Windows.Input;

namespace EzTabs.Presentation.Services.DomainServices;

public class TabService : BaseService<Tab>
{
    public static Tab? SavedTab { get; private set; }

    public TabService(EzTabsContext context, INavigationService navigationService) : base(context, navigationService)
    {

    }

    public async Task GoToTab(Guid tabId)
    {
        SavedTab = await _repository!.GetById(tabId);
        if (SavedTab is null) throw new ArgumentNullException(nameof(SavedTab));
        await AddViewToTab(SavedTab);
        NavigationService.NavigateTo<TabControlViewModel>();
    }
    
    public async Task GoEditTab(Guid tabId)
    {
        SavedTab = await _repository!.GetById(tabId);
        NavigationService.NavigateTo<TabEditingControlViewModel>();
    }

    public async Task<Tab?> CreateTab(Guid authorId, string title, string band, string genre, string key, int bpm, string description)
    {
        var allTabs = await _repository!.GetAll();
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

    public async Task SaveTabText(string tabText, List<List<List<string>>> tabTextInList)
    {
        SavedTab!.TabText = tabText;
        SavedTab!.JsonTabText = JsonSerializer.Serialize(tabTextInList);
        await _repository.Update(SavedTab!);
    }

    public async Task UpdateTabRating(Guid tabId, List<TabRate> tabRates)
    {
        double avgRate = 0;

        foreach(var tabRate in tabRates)
        {
            avgRate += tabRate.Rate;
        }

        Tab? tab = await _repository.GetById(tabId); 

        if (tab != null)
        {
            if (tabRates.Count != 0) 
            tab.Rating = avgRate /= tabRates.Count;
            else tab.Rating = 0;
            await _repository.Update(tab);
        }
    }

    public async Task AddViewToTab(Tab tab)
    {
        tab.AddView();
        await _repository.Update(tab);
    }
}
