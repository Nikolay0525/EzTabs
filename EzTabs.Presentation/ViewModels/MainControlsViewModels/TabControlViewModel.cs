using CommunityToolkit.Mvvm.Input;
using EzTabs.Data.Domain;
using EzTabs.Presentation.Resources.Styles.CustomTypes;
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels;

public class TabControlViewModel : BaseViewModel
{
    public BaseViewModel ControlBarViewModel { get; private set; }

    private readonly IWindowService _windowService;
    private UserService _userService;
    private SearchingService _searchingService;

    private int _currentPage = 0;
    private bool _nextPageEnabled = false;
    private bool _previousPageEnabled = false;

    private bool _isZoomOpen = false;
    private bool _isInfoOpen = false;
    private bool _isSortByOpen = false;
    private bool _isCommentSectionVisible = false;

    private int _zoom = 100;
    private double _fontSize = 33.3;
    private string _tabText;
    private string _titleBand;

    private string _title;
    private string _band;
    private string _authorName;
    private string _genre;
    private string _key;
    private string _bpm;
    private string _description;

    private ObservableCollection<ComboButtonControl> _listOfSortByOptions = new();
    private string _selectedOrderByOptionText = "Order By";
    private ObservableCollection<Comment> _selected = "Order By";
    private SortByOption _selectedOrderByOption = 0;

    private Dictionary<string, SortByOption> SortByOptions { get; } = new()
    {
        { "Most Popular", SortByOption.Popularity},
        { "Newest", SortByOption.Newest }
    };

    public ObservableCollection<ComboButtonControl> ListOfSortByOptions 
    { 
        get => _listOfSortByOptions; 
        set => _listOfSortByOptions = value; 
    }
    
    public string SelectedOrderByOption
    { 
        get => _selectedOrderByOptionText; 
        set 
        {
            _selectedOrderByOptionText = value;
            _selectedOrderByOption = SortByOptions.GetValueOrDefault(value);
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

    public bool IsZoomOpen
    {
        get => _isZoomOpen;
        set
        {
            _isZoomOpen = value;
            OnPropertyChanged();
        }
    }
    public bool IsInfoOpen
    {
        get => _isInfoOpen;
        set
        {
            _isInfoOpen = value;
            OnPropertyChanged();
        }
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
    public bool IsCommentSectionVisible
    {
        get => _isCommentSectionVisible;
        set
        {
            _isCommentSectionVisible = value;
            OnPropertyChanged();
        }
    }

    public string TabText
    {
        get => _tabText;
        private set
        {
            _tabText = value;
            OnPropertyChanged();
        }
    }
    
    public string TitleBand
    {
        get => _titleBand;
        private set
        {
            _titleBand = value;
            OnPropertyChanged();
        }
    }

    public int Zoom
    {
        get => _zoom;
        set
        {
            _zoom = value;
            FontSize = value / 3;
            OnPropertyChanged();
        }
    }

    public double FontSize
    {
        get => _fontSize;
        set
        {
            _fontSize = value;
            OnPropertyChanged();
            UpdateTabText();
        }
    }

    public string Title 
    { 
        get => _title;
        private set
        {
            _title = value;
            OnPropertyChanged();
        }
    }
    public string Band 
    { 
        get => _band; 
        private set 
        { 
            _band = value;
            OnPropertyChanged();
        }
    }
    public string AuthorName 
    { 
        get => _authorName;
        private set
        {
            _authorName = value;
            OnPropertyChanged();
        }
    }
    public string Genre 
    { 
        get => _genre;
        private set
        {
            _genre = value;
            OnPropertyChanged();
        }
    }
    public string Key 
    { 
        get => _key;
        private set
        {
            _key = value;
            OnPropertyChanged();
        }
    }
    public string Bpm 
    { 
        get => _bpm;
        set
        {
            _bpm = value;
            OnPropertyChanged();
        }
    }
    public string Description 
    { 
        get => _description;
        private set
        {
            _description = value;
            OnPropertyChanged();
        }
    }

    public ICommand GoToSearchCommand { get; }
    public ICommand HandleSortByCommand { get; }
    public ICommand ShowSortByOptionsCommand { get; }
    public int CurrentPage { get => _currentPage; set => _currentPage = value; }

    public TabControlViewModel(IViewModelService viewModelService, INavigationService navigationService, IWindowService windowService, UserService userService, SearchingService searchingService)  : base(viewModelService, navigationService)
    {
        _windowService = windowService;
        _userService = userService;
        _searchingService = searchingService;
        GoToSearchCommand = new RelayCommand(GoToSearchControl);
        HandleSortByCommand = new RelayCommand<string>(HandleSortBy);
        ControlBarViewModel = viewModelService.CreateViewModel<ControlBarViewModel>();
        UpdateTabText();
        UpdateSortByList();
        Task.Run(UpdateAuthorName);
    }

    private void UpdateSortByList()
    {
        foreach (var option in SortByOptions)
        {
            var button = new ComboButtonControl()
            {
                Command = this.HandleSortByCommand,
                CommandParameter = $"{option}",
                Text = $"{option}"
            };

            ListOfSortByOptions.Add(button);
        }
    }

    private void HandleSortBy(string? selectedOption)
    {
        if (selectedOption is null) throw new ArgumentNullException(nameof(selectedOption));
        SelectedOrderByOption = selectedOption;
        IsSortByOpen = false;
    }

    
    private void GoToSearchControl()
    {
        NavigationService.NavigateTo<SearchControlViewModel>();
    }

    private void UpdateTabText()
    {
        Title = TabService.SavedTab!.Title;
        Band = TabService.SavedTab!.Band;
        Genre = TabService.SavedTab!.Genre;
        Key = TabService.SavedTab!.Key;
        Bpm = TabService.SavedTab!.BitsPerMinute.ToString();
        Description = TabService.SavedTab!.Description;
        TabText = TabService.SavedTab!.TabText;
    }

    private async Task UpdateAuthorName()
    {
        var foundedUser = await _userService.FindUserById(TabService.SavedTab!.AuthorId);
        if (foundedUser is null) return;
        AuthorName = foundedUser.Name;
        TitleBand = TabService.SavedTab!.Title + " - " + TabService.SavedTab!.Band + " by " + "@" + foundedUser.Name;
    }

    private void UpdateCommentsList()
    {
        List<Comment> commentsToDisplay = _searchingService.ShowComments(_windowService.WindowHeight, _currentPage, _selectedOrderByOption, null);

        if (commentsToDisplay.Count > (_windowService.WindowHeight - 200) / 80)
        {
            NextPageEnabled = true;
        }
        else { NextPageEnabled = false; }
        .Clear();

        TabsInSearchList = AddTabsInSearchList(tabsToDisplay);
    }

    private ObservableCollection<TabInSearchPageControl> AddTabsInSearchList(List<Tab> tabsToDisplay)
    {
        ObservableCollection<TabInSearchPageControl> tabInSearchPageControls = new();

        foreach (Tab tab in tabsToDisplay)
        {

            TabInSearchPageControl tabItem = new()
            {
                DataContext = this,
                TabId = tab.Id,
                Text = tab.Band + " - " + tab.Title
            };
            if (tab.AuthorId == UserService.SavedUser.Id) tabItem.CanBeEdited = true;
            tabInSearchPageControls.Add(tabItem);
        }

        return tabInSearchPageControls;
    }
}
