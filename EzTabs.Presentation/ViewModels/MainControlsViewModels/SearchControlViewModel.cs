using CommunityToolkit.Mvvm.Input;
using EzTabs.Data.Domain;
using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.SearchingServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.Services.ViewServices;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using EzTabs.Presentation.ViewModels.MainControlsViewModels.SimpleControlsViewModels.ControlBarPartsVMs;
using EzTabs.Presentation.Views.MainControls.SimpleControls;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels;

public class SearchControlViewModel : BaseViewModel
{
    private ObservableCollection<TabInSearchPageControl> _tabsInSearchList = new();
    private string _searchString;
    private int _currentPage = 0;
    private bool _isFilterEnabled = false;
    private double _filterRowHeight = 0;
    private bool _firstPageVisibility = false;
    private bool _nextPageEnabled = false;
    private bool _previousPageEnabled = false;

    private readonly IWindowService _windowService;

    public bool IsFilterEnabled
    {
        get => _isFilterEnabled;
        set 
        {
            _isFilterEnabled = value;
            OnPropertyChanged();
            FilterRowHeight = IsFilterEnabled ? 45 : 0;
        }
    }
    
    public double FilterRowHeight
    {
        get => _filterRowHeight;
        set 
        {
            _filterRowHeight = value;
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<TabInSearchPageControl> TabsInSearchList
    {
        get => _tabsInSearchList;
        set => _tabsInSearchList = value;
    }

    public string SearchString
    {
        get => _searchString;
        set
        {
            _searchString = value;
            OnPropertyChanged();
            UpdateSearchList();
        }
    }

    public int CurrentPage
    {
        get => _currentPage + 1;
        set
        {
            _currentPage = value;
            OnPropertyChanged();
            if (_currentPage > 0) PreviousPageEnabled = true;
            else PreviousPageEnabled = false;
            UpdateSearchList();
        }
    }

    public bool FirstPageVisibility 
    { 
        get => _firstPageVisibility; 
        set 
        {
            _firstPageVisibility = value;
            OnPropertyChanged();
        } 
    }
    
    public bool NextPageEnabled
    { 
        get => _nextPageEnabled; 
        set 
        {
            _nextPageEnabled = value;
            OnPropertyChanged();
        } 
    }

    public bool PreviousPageEnabled 
    { 
        get => _previousPageEnabled;
        set 
        {
            _previousPageEnabled = value;
            OnPropertyChanged();
        }
    }

    public ICommand NextPageCommand { get; }
    public ICommand PreviousPageCommand { get; }
    public ICommand OnTheFirstPageCommand { get; }
    public ICommand SwitchFilterCommand { get; }
    public ICommand GoToCreationOfTabCommand { get; }
    public ICommand GoToClickedTabCommand { get; }
    public ICommand GoEditClickedTabCommand { get; }

    private readonly TabService _tabService;
    private readonly SearchingService _searchingService;

    public BaseViewModel ControlBarViewModel { get; private set; }


    public SearchControlViewModel(INavigationService navigationService ,IViewModelService viewModelService, TabService tabService, SearchingService searchingService, IWindowService windowService) : base(viewModelService, navigationService)
    {
        _windowService = windowService;
        _tabService = tabService;
        _searchingService = searchingService;
        UpdateSearchList();
        ControlBarViewModel = ViewModelService.CreateViewModel<ControlBarViewModel>();
        NextPageCommand = new RelayCommand(NextPage);
        PreviousPageCommand = new RelayCommand(PreviousPage);
        OnTheFirstPageCommand = new RelayCommand(OnTheFirstPage);
        SwitchFilterCommand = new RelayCommand(SwitchFilter);
        GoToCreationOfTabCommand = new RelayCommand(GoToCreationOfTab);
        GoToClickedTabCommand = new AsyncRelayCommand<Guid>(GoToClickedTab);
        GoEditClickedTabCommand = new AsyncRelayCommand<Guid>(GoEditClickedTab);
    }

    private async Task GoToClickedTab(Guid tabId)
    {
       await _tabService.GoToTab(tabId);
    }
    
    private async Task GoEditClickedTab(Guid tabId)
    {
       await _tabService.GoEditTab(tabId);
    }

    private void NextPage()
    {
        int add = ++_currentPage;
        CurrentPage = add;
    }
    
    private void PreviousPage()
    {
        int minus = --_currentPage;
        CurrentPage = minus;
    }
    
    private void OnTheFirstPage()
    {
        CurrentPage = 0;
    }

    private async Task UpdateSearchList()
    {
        List<Tab> tabsToDisplay = await _searchingService.SearchTabs(_windowService.WindowHeight - 200, _currentPage, _searchString);

        if (tabsToDisplay.Count > (_windowService.WindowHeight - 200) / 40)
        {
            NextPageEnabled = true;
        }
        else { NextPageEnabled = false; }

        TabsInSearchList.Clear();

        foreach(Tab tab in tabsToDisplay)
        {
            
            TabInSearchPageControl tabItem = new()
            {
                DataContext = this,
                TabId = tab.Id,
                Text = tab.Band + " - " + tab.Title
            };
            if (tab.AuthorId == UserService.SavedUser.Id) tabItem.CanBeEdited = true;
            TabsInSearchList.Add(tabItem);
        }
    }
    
    private void SwitchFilter()
    {
        IsFilterEnabled = !IsFilterEnabled;
    }

    private void GoToCreationOfTab()
    {
        NavigationService.NavigateTo<TabCreationControlViewModel>();
    }
}
