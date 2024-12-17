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
        var operation = await _repository!.GetById(tabId);
        if (!operation.Success) throw new InvalidOperationException(operation.ErrorMessage);
        SavedTab = operation.Data!;
        if (SavedTab is null) throw new ArgumentNullException(nameof(SavedTab));
        await AddViewToTab(SavedTab);
        NavigationService.NavigateTo<TabControlViewModel>();
    }
    
    public async Task GoEditTab(Guid tabId)
    {
        var operation = await _repository!.GetById(tabId);
        if (!operation.Success) throw new InvalidOperationException(operation.ErrorMessage);
        SavedTab = operation.Data!;
        NavigationService.NavigateTo<TabEditingControlViewModel>();
    }

    public async Task<Tab?> CreateTab(Guid authorId, string title, string band, string genre, string key, int bpm, string description)
    {
        var allTabs = await _repository!.GetAll();
        if (!allTabs.Success) throw new InvalidOperationException(allTabs.ErrorMessage);
        if (allTabs.Data!.FirstOrDefault(t => t.AuthorId == authorId && t.Title == title && t.Band == band && t.Genre == genre) != null) return null;
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
        var operation = await _repository.Add(newTab);
        if (!operation.Success) throw new InvalidOperationException(operation.ErrorMessage);

        SavedTab = newTab;
        return newTab;
    }

    public async Task SaveTabText(string tabText, List<List<List<string>>> tabTextInList)
    {
        SavedTab!.TabText = tabText;
        SavedTab!.JsonTabText = JsonSerializer.Serialize(tabTextInList);
        var operation = await _repository.Update(SavedTab!);
        if (!operation.Success) throw new InvalidOperationException(operation.ErrorMessage);
    }

    public static void LoadBackup()
    {
        SavedTab!.TabText = SavedTab.AutoSavedTabText;
        SavedTab!.JsonTabText = SavedTab.JsonAutoSavedTabText;
    }
    
    public async Task MakeBackup(string tabText, List<List<List<string>>> tabTextInList)
    {
        SavedTab!.AutoSavedTabText = tabText;
        SavedTab!.JsonAutoSavedTabText = JsonSerializer.Serialize(tabTextInList);
        SavedTab!.DateOfBackup = DateTime.Now;
        var operation = await _repository.Update(SavedTab!);
        if (!operation.Success) throw new InvalidOperationException(operation.ErrorMessage);
    }
    
    public async Task DeleteTab()
    {
        var operation = await _repository.Delete(SavedTab!);
        if (!operation.Success) throw new InvalidOperationException(operation.ErrorMessage);
    }

    public async Task UpdateTabRating(Guid tabId, List<TabRate> tabRates)
    {
        double avgRate = 0;

        foreach(var tabRate in tabRates)
        {
            avgRate += tabRate.Rate;
        }

        var operation = await _repository.GetById(tabId);
        if (!operation.Success) throw new InvalidOperationException(operation.ErrorMessage);

        Tab? tab = operation.Data;

        if (tab != null)
        {
            if (tabRates.Count != 0) 
            tab.Rating = avgRate /= tabRates.Count;
            else tab.Rating = 0;
            var op = await _repository.Update(tab);
            if (!op.Success) throw new InvalidOperationException(op.ErrorMessage);
        }
    }

    public async Task AddViewToTab(Tab tab)
    {
        tab.AddView();
        var operation = await _repository.Update(tab);
        if (!operation.Success) throw new InvalidOperationException(operation.ErrorMessage);
    }
}
