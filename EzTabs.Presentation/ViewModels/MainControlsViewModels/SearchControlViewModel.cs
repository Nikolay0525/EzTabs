using CommunityToolkit.Mvvm.Input;
using EzTabs.Data.Domain;
using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.SearchingServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.Services.ViewServices;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using EzTabs.Presentation.ViewModels.MainControlsViewModels.Enums;

using EzTabs.Presentation.Views.MainControls.SimpleControls;
using EzTabs.Presentation.Views.MainControls.SimpleControls.ControlBarParts.DropControls;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels;

public class SearchControlViewModel : BaseViewModel
{

    private readonly TabService _tabService;
    private readonly FavouriteTabService _favouriteTabService;
    private readonly SearchingService _searchingService;
    private readonly IWindowService _windowService;

    private List<TabInSearchPageControl> _tabsInSearchList = new();

    private bool _firstPageVisibility = false;
    private bool _previousPageEnabled = false;
    private bool _nextPageEnabled = false;
    private bool _isFilterEnabled = false;
    private bool _isSortByOpen = false;
    private bool _isSearchByOpen = false;
    private bool _onlyMineTabs = false;
    private bool _onlyFavouriteTabs = false;
    private bool _isAuthorNameVisible = false;

    private System.Timers.Timer _debounceTimer;
    private string _authorName;
    private string _searchString;
    private int _currentPage = 0;
    private double _filterRowHeight = 0;

    private SearchByOption _selectedSearchByOption = 0;
    private SortByOption _selectedSortByOption = 0;
    private string _selectedSearchByOptionText = "Search By";
    private string _selectedSortByOptionText = "Sort By";
    private ObservableCollection<ComboButtonControl> _listOfSearchByOptions = new();
    private ObservableCollection<ComboButtonControl> _listOfSortByOptions = new();

    private Dictionary<string,SearchByOption> _searchByOptions { get; } = new()
    {
        {"Song & Band", SearchByOption.BandTitle},
        {"Song & Author", SearchByOption.SongAuthor}
    };
    
    private Dictionary<string,SortByOption> _sortByOptions { get; } = new()
    {
        {"Most Popular" ,SortByOption.Popularity },
        {"Highly Rated" , SortByOption.Rating },
        {"Newest" , SortByOption.Newest }
    };

    public ObservableCollection<ComboButtonControl> ListOfSearchByOptions
    {
        get => _listOfSearchByOptions;
        set => _listOfSearchByOptions = value;
    }
    
    public ObservableCollection<ComboButtonControl> ListOfSortByOptions
    {
        get => _listOfSortByOptions;
        set => _listOfSortByOptions = value;
    }

    public bool IsSortByOpen
    { 
        get => _isSortByOpen; 
        set 
        {
            _isSortByOpen = value;
            OnPropertyChanged();
        }
    }
    
    public bool IsSearchByOpen
    { 
        get => _isSearchByOpen; 
        set 
        {
            _isSearchByOpen = value;
            OnPropertyChanged();
        }
    }
    
    public bool IsAuthorNameVisible
    {
        get => _isAuthorNameVisible;
        set 
        {
            _isAuthorNameVisible = value;
            OnPropertyChanged();
        }
    }

    public string AuthorName
    {
        get => _authorName;
        set
        {
            _authorName = value;
            OnPropertyChanged();
            StartDebounceTimer();
        }
    }

    public string SelectedSearchByOption
    { 
        get => _selectedSearchByOptionText; 
        set 
        {
            _selectedSearchByOptionText = value;
            _selectedSearchByOption = _searchByOptions.GetValueOrDefault(value);
            if (_selectedSearchByOption == SearchByOption.SongAuthor) IsAuthorNameVisible = true;
            else IsAuthorNameVisible = false;
            OnPropertyChanged();
            PerformSearch();
        }
    }
    
    public string SelectedSortByOption
    { 
        get => _selectedSortByOptionText; 
        set 
        {
            _selectedSortByOptionText = value;
            _selectedSortByOption = _sortByOptions.GetValueOrDefault(value);
            OnPropertyChanged();
            PerformSearch();
        }
    }
    

    public bool OnlyFavouriteTabs 
    { 
        get => _onlyFavouriteTabs; 
        set 
        {
            _onlyFavouriteTabs = value;
            OnPropertyChanged();
            PerformSearch();
        }
    }

    public bool IsFilterEnabled
    {
        get => _isFilterEnabled;
        set 
        {
            _isFilterEnabled = value;
            FilterRowHeight = IsFilterEnabled ? 45 : 0;
            OnPropertyChanged();
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
    
    public List<TabInSearchPageControl> TabsInSearchList
    {
        get => _tabsInSearchList;
        set
        {
            _tabsInSearchList = value;
            OnPropertyChanged();
        }
    }

    public string SearchString
    {
        get => _searchString;
        set
        {
            _searchString = value;
            OnPropertyChanged();
            StartDebounceTimer();
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

    public BaseViewModel ControlBarViewModel { get; private set; }

    public ICommand CreateTabCommand { get; }
    public ICommand PerformSearchCommand { get; }
    public ICommand SendFavouriteStateCommand { get; }
    public ICommand HandleSearchByCommand { get; }
    public ICommand HandleSortByCommand { get; }
    public ICommand NextPageCommand { get; }
    public ICommand PreviousPageCommand { get; }
    public ICommand OnTheFirstPageCommand { get; }
    public ICommand SwitchFilterCommand { get; }
    public ICommand GoToCreationOfTabCommand { get; }
    public ICommand GoToClickedTabCommand { get; }
    public ICommand GoEditClickedTabCommand { get; }


    public SearchControlViewModel(INavigationService navigationService ,IViewModelService viewModelService, IWindowService windowService, 
        TabService tabService, FavouriteTabService favouriteTabService, SearchingService searchingService) : base(viewModelService, navigationService)
    {
        _windowService = windowService;
        _tabService = tabService;
        _favouriteTabService = favouriteTabService;
        _searchingService = searchingService;
        CreateTabCommand = new RelayCommand(NavigateToCreationOfTab);
        PerformSearchCommand = new RelayCommand(PerformSearch);
        SendFavouriteStateCommand = new AsyncRelayCommand<object?>(SendFavouriteState);
        HandleSearchByCommand = new RelayCommand<string>(HandleSearchBy);
        HandleSortByCommand = new RelayCommand<string>(HandleSortBy);
        NextPageCommand = new AsyncRelayCommand(NextPage);
        PreviousPageCommand = new AsyncRelayCommand(PreviousPage);
        OnTheFirstPageCommand = new AsyncRelayCommand(OnTheFirstPage);
        GoToCreationOfTabCommand = new RelayCommand(GoToCreationOfTab);
        GoToClickedTabCommand = new AsyncRelayCommand<Guid>(GoToClickedTab);
        GoEditClickedTabCommand = new AsyncRelayCommand<Guid>(GoEditClickedTab);

        ButtonInDropControl createTab = new()
        {
            IsButtonVisible = true,
            Text = "Create Tab",
            ButtonCommand = CreateTabCommand
        };
        ButtonsInMenu.Insert(0, createTab);

        _debounceTimer = new System.Timers.Timer(1000);
        _debounceTimer.Elapsed += OnDebounceTimerElapsed;
        _debounceTimer.AutoReset = false;

        StartDebounceTimer();
        UpdateComboLists();
    }

    private void NavigateToCreationOfTab()
    {
        NavigationService.NavigateTo<TabCreationControlViewModel>();
    }

    private async Task SendFavouriteState(object? parameter)
    {
        if (parameter is Tuple<Guid, bool> args) // where Item1 is id of comment and Item2 is state of UI element
        {
            await _favouriteTabService.UpdateStateOfTab(UserService.SavedUser.Id, args.Item1, args.Item2);
        }
    }

    private void UpdateComboLists()
    {
        foreach (var option in _sortByOptions)
        {
            var button = new ComboButtonControl()
            {
                Command = this.HandleSortByCommand,
                CommandParameter = option.Key,
                Text = $"{option.Key}"
            };

            ListOfSortByOptions.Add(button);
        }

        foreach (var option in _searchByOptions)
        {
            var button = new ComboButtonControl()
            {
                Command = this.HandleSearchByCommand,
                CommandParameter = option.Key,
                Text = $"{option.Key}"
            };

            ListOfSearchByOptions.Add(button);
        }
    }

    private void HandleSearchBy(string? selectedOption)
    {
        if (selectedOption == null) return;
        SelectedSearchByOption = selectedOption;
        IsSearchByOpen = false;
    }

    private void HandleSortBy(string? selectedOption)
    {
        if (selectedOption == null) return;
        SelectedSortByOption = selectedOption;
        IsSortByOpen = false;
    }


    private async Task GoToClickedTab(Guid tabId)
    {
        await _tabService.GoToTab(tabId);
    }
    
    private async Task GoEditClickedTab(Guid tabId)
    {
        await _tabService.GoEditTab(tabId);
    }

    private async Task NextPage()
    {
        int add = ++_currentPage;
        CurrentPage = add;
        if (_currentPage == 0) FirstPageVisibility = false; else FirstPageVisibility = true;
        await UpdateSearchList();
    }

    private async Task PreviousPage()
    {
        int minus = --_currentPage;
        CurrentPage = minus;
        if(_currentPage == 0) FirstPageVisibility = false; else FirstPageVisibility = true;
        await UpdateSearchList();
    }
    
    private async Task OnTheFirstPage()
    {
        CurrentPage = 0;
        FirstPageVisibility = false;
        await UpdateSearchList();
    }

    private async void PerformSearch()
    {
        await UpdateSearchList();
    }

    private void StartDebounceTimer()
    {
        _debounceTimer.Stop();
        _debounceTimer.Start();
    }

    private void OnDebounceTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        Application.Current.Dispatcher.Invoke(PerformSearch);
    }

    private async Task UpdateSearchList()
    {
        var tabsToDisplay = await _searchingService.SearchTabs((int)(_windowService.WindowHeight / 70), _currentPage, _searchString, UserService.SavedUser.Id, OnlyFavouriteTabs, _selectedSearchByOption, _selectedSortByOption, _authorName);

        if (tabsToDisplay.Item2 > 0)
        {
            NextPageEnabled = true;
        }
        else { NextPageEnabled = false; }
        TabsInSearchList.Clear();

        TabsInSearchList = await AddTabsInSearchList(tabsToDisplay.Item1);
    }
    
    private async Task<List<TabInSearchPageControl>> AddTabsInSearchList(List<Tab> tabsToDisplay)
    {
        List<TabInSearchPageControl> tabInSearchPageControls = new();

        foreach (Tab tab in tabsToDisplay)
        {
            TabInSearchPageControl tabItem = new()
            {
                DataContext = this,
                TabId = tab.Id,
                Text = tab.Band + " - " + tab.Title,
                Rating = tab.Rating,
                Favourite = false
            };
            if (tab.AuthorId == UserService.SavedUser.Id) tabItem.CanBeEdited = true;
            tabInSearchPageControls.Add(tabItem);
            bool favourite = await _favouriteTabService.IsTabFavouriteByIds(UserService.SavedUser.Id, tab.Id);
            tabItem.Favourite = favourite;
        }

        return tabInSearchPageControls;
    }

    private void GoToCreationOfTab()
    {
        NavigationService.NavigateTo<TabCreationControlViewModel>();
    }
}
