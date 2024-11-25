using EzTabs.Data;
using EzTabs.Data.Domain;
using EzTabs.Data.Repository;
using EzTabs.Presentation.Services.ContextServices;
using EzTabs.Presentation.Services.DomainServices.BaseServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.ViewModels.MainControlsViewModels;

namespace EzTabs.Presentation.Services.DomainServices;

public class TabService : BaseService<Tab>
{
    public static Tab? SavedTab { get; private set; }

    public TabService(IContextFactoryService contextFactoryService, INavigationService navigationService) : base(contextFactoryService, navigationService)
    {

    }

    public async Task GoToTab(Guid tabId)
    {
        await EnsureRepoCreated();

        SavedTab = await _repository!.GetById(tabId);
        NavigationService.NavigateTo<TabControlViewModel>();
    }
    
    public async Task GoEditTab(Guid tabId)
    {
        await EnsureRepoCreated();

        SavedTab = await _repository!.GetById(tabId);
        NavigationService.NavigateTo<TabEditingControlViewModel>();
    }

    public async Task<Tab?> CreateTab(Guid authorId, string title, string band, string genre, string key, int bpm, string description)
    {
        await EnsureRepoCreated();

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
}
