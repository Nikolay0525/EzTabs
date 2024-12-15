using CommunityToolkit.Mvvm.Input;
using EzTabs.Data.Domain;
using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.SearchingServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.Services.ViewServices;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using EzTabs.Presentation.ViewModels.MainControlsViewModels.Enums;
using EzTabs.Presentation.ViewModels.MainControlsViewModels.SimpleControlsViewModels.ControlBarPartsVMs;
using EzTabs.Presentation.Views.MainControls.SimpleControls;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels;

public class SearchControlViewModel : BaseViewModel
{
    private readonly TabService _tabService;
    private readonly SearchingService _searchingService;
    private List<TabInSearchPageControl> _tabsInSearchList = new();
    private readonly IWindowService _windowService;

    private bool _firstPageVisibility = false;
    private bool _previousPageEnabled = false;
    private bool _nextPageEnabled = false;
    private bool _isFilterEnabled = false;
    private bool _isSortByOpen = false;
    private bool _isSearchByOpen = false;
    private bool _onlyMineTabs = false;
    private bool _onlyFavouriteTabs = false;
    private bool _isAuthorNameVisible = false;

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
        }
    }
    

    public bool OnlyFavouriteTabs 
    { 
        get => _onlyFavouriteTabs; 
        set 
        {
            _onlyFavouriteTabs = value;
            OnPropertyChanged();
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

    public BaseViewModel ControlBarViewModel { get; private set; }

    public ICommand HandleSearchByCommand { get; }
    public ICommand HandleSortByCommand { get; }
    public ICommand NextPageCommand { get; }
    public ICommand PreviousPageCommand { get; }
    public ICommand OnTheFirstPageCommand { get; }
    public ICommand SwitchFilterCommand { get; }
    public ICommand GoToCreationOfTabCommand { get; }
    public ICommand GoToClickedTabCommand { get; }
    public ICommand GoEditClickedTabCommand { get; }


    public SearchControlViewModel(INavigationService navigationService ,IViewModelService viewModelService, IWindowService windowService, TabService tabService, SearchingService searchingService) : base(viewModelService, navigationService)
    {
        _windowService = windowService;
        _tabService = tabService;
        _searchingService = searchingService;
        ControlBarViewModel = ViewModelService.CreateViewModel<ControlBarViewModel>();
        HandleSearchByCommand = new RelayCommand<string>(HandleSearchBy);
        HandleSortByCommand = new RelayCommand<string>(HandleSortBy);
        NextPageCommand = new RelayCommand(NextPage);
        PreviousPageCommand = new RelayCommand(PreviousPage);
        OnTheFirstPageCommand = new RelayCommand(OnTheFirstPage);
        GoToCreationOfTabCommand = new RelayCommand(GoToCreationOfTab);
        GoToClickedTabCommand = new AsyncRelayCommand<Guid>(GoToClickedTab);
        GoEditClickedTabCommand = new AsyncRelayCommand<Guid>(GoEditClickedTab);
        UpdateComboLists();
        UpdateSearchList();
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
        SelectedSearchByOption = selectedOption;
        IsSearchByOpen = false;
    }

    private void HandleSortBy(string? selectedOption)
    {
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

    private void NextPage()
    {
        int add = ++_currentPage;
        CurrentPage = add;
        if (_currentPage == 0) FirstPageVisibility = false; else FirstPageVisibility = true;
    }

    private void PreviousPage()
    {
        int minus = --_currentPage;
        CurrentPage = minus;
        if(_currentPage == 0) FirstPageVisibility = false; else FirstPageVisibility = true;
    }
    
    private void OnTheFirstPage()
    {
        CurrentPage = 0;
    }

    private async void UpdateSearchList()
    {
        var tabsToDisplay = await _searchingService.SearchTabs((int)(_windowService.WindowHeight/80), _currentPage, _searchString, _selectedSearchByOption, _selectedSortByOption, _authorName);

        if (tabsToDisplay.Item2 > 0)
        {
            NextPageEnabled = true;
        }
        else { NextPageEnabled = false; }
        TabsInSearchList.Clear();

        TabsInSearchList = AddTabsInSearchList(tabsToDisplay.Item1);
    }
    
    private List<TabInSearchPageControl> AddTabsInSearchList(List<Tab> tabsToDisplay)
    {
        List<TabInSearchPageControl> tabInSearchPageControls = new();

        foreach (Tab tab in tabsToDisplay)
        {
            TabInSearchPageControl tabItem = new()
            {
                DataContext = this,
                TabId = tab.Id,
                Text = tab.Band + " - " + tab.Title,
                Rating = tab.Rating
            };
            if (tab.AuthorId == UserService.SavedUser.Id) tabItem.CanBeEdited = true;
            tabInSearchPageControls.Add(tabItem);
        }

        return tabInSearchPageControls;
    }

    private void GoToCreationOfTab()
    {
        NavigationService.NavigateTo<TabCreationControlViewModel>();
    }
}
