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
using static EzTabs.Presentation.Views.MainControls.SimpleControls.CommentControl;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels;

public class TabControlViewModel : BaseViewModel
{
    public BaseViewModel ControlBarViewModel { get; private set; }

    private readonly IWindowService _windowService;
    private UserService _userService;
    private SearchingService _searchingService;
    private CommentService _commentService;
    private CommentRateService _commentRateService;

    private int _currentPage = 0;
    private bool _nextPageEnabled = false;
    private bool _previousPageEnabled = false;

    private bool _isZoomOpen = false;
    private bool _isInfoOpen = false;
    private bool _isSortByOpen = false;
    private bool _isCommentSectionVisible = false;
    private bool _isMessageWritingVisible = false;
    private bool _isPageButtonsVisible = true;
    private bool _firstPageVisibility = false;

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
    private List<Comment> _listOfCommentsToShow = new();
    private List<CommentControl> _commentsToShow = new();
    private string _selectedOrderByOptionText = "Order By";
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

    public List<CommentControl> CommentsToShow
    { 
        get => _commentsToShow; 
        set 
        {
            _commentsToShow = value;
            OnPropertyChanged();
        }
    }
    
    public string SelectedOrderByOption
    { 
        get => _selectedOrderByOptionText; 
        set 
        {
            _selectedOrderByOptionText = value;
            _selectedOrderByOption = SortByOptions.GetValueOrDefault(value);
            UpdateCommentsList();
            OnPropertyChanged();
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
            UpdateCommentsList();
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
    public bool IsMessageWritingVisible
    {
        get => _isMessageWritingVisible;
        set
        {
            _isMessageWritingVisible = value;
            IsPageButtonsVisible = !_isMessageWritingVisible;
            OnPropertyChanged();
        }
    }
    public bool IsPageButtonsVisible
    {
        get => _isPageButtonsVisible;
        private set
        {
            _isPageButtonsVisible = value;
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

    public ICommand SubmitCommentCommand { get; }
    public ICommand SubmitReplyCommand { get; }
    public ICommand SendLikeStateCommand { get; }
    public ICommand NextPageCommand { get; }
    public ICommand PreviousPageCommand { get; }
    public ICommand OnTheFirstPageCommand { get; }
    public ICommand GoToSearchCommand { get; }
    public ICommand HandleSortByCommand { get; }
    public ICommand ShowSortByOptionsCommand { get; }

    public TabControlViewModel(IViewModelService viewModelService, INavigationService navigationService, IWindowService windowService, 
        UserService userService, SearchingService searchingService, CommentRateService commentRateService, CommentService commentService)  : base(viewModelService, navigationService)
    {
        _windowService = windowService;
        _userService = userService;
        _searchingService = searchingService;
        _commentRateService = commentRateService;
        _commentService = commentService;
        SubmitCommentCommand = new AsyncRelayCommand<string>(SubmitComment);
        SubmitReplyCommand = new AsyncRelayCommand<object>(SubmitReply);
        SendLikeStateCommand = new AsyncRelayCommand<object>(SendLikeState);
        NextPageCommand = new RelayCommand(NextPage);
        PreviousPageCommand = new RelayCommand(PreviousPage);
        OnTheFirstPageCommand = new RelayCommand(OnTheFirstPage);
        GoToSearchCommand = new RelayCommand(GoToSearchControl);
        HandleSortByCommand = new RelayCommand<string>(HandleSortBy);
        ControlBarViewModel = viewModelService.CreateViewModel<ControlBarViewModel>();
        UpdateCommentsList();
        UpdateTabText();
        UpdateSortByList();
        Task.Run(UpdateAuthorName);
    }

    private async Task SubmitComment(string? messageText)
    { 
        if(messageText is null) await _commentService.CreateComment(UserService.SavedUser.Id, TabService.SavedTab!.Id, string.Empty, Guid.Empty);
        await _commentService.CreateComment(UserService.SavedUser.Id, TabService.SavedTab!.Id, messageText!, Guid.Empty);
    }
    
    private async Task SubmitReply(object? message)
    {
        //await _commentService.CreateComment(UserService.SavedUser.Id, TabService.SavedTab!.Id, message.text, message.commentId);
    }

    private async Task SendLikeState(object? parameter)
    {
        if(parameter is Tuple<Guid, bool> args)
        {
            await _commentRateService.ApplyCommentRate(UserService.SavedUser.Id, args.Item1, args.Item2);
        }
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
        if (_currentPage == 0) FirstPageVisibility = false; else FirstPageVisibility = true;
    }

    private void OnTheFirstPage()
    {
        CurrentPage = 0;
    }

    private void UpdateSortByList()
    {
        foreach (var option in SortByOptions)
        {
            var button = new ComboButtonControl()
            {
                Command = this.HandleSortByCommand,
                CommandParameter = option.Key,
                Text = $"{option.Key}"
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
        List<Comment> commentsToDisplay = _searchingService.ShowComments((_windowService.WindowHeight - (_windowService.WindowHeight / 2))/160, _currentPage, _selectedOrderByOption);

        if (commentsToDisplay.Count > (_windowService.WindowHeight - _windowService.WindowHeight/2) / 160)
        {
            NextPageEnabled = true;
        }
        else { NextPageEnabled = false; }
        CommentsToShow.Clear();
        CommentsToShow = AddCommentsInCommentsList(commentsToDisplay);
        UpdateCommentsInformation(commentsToDisplay);
    }

    private List<CommentControl> AddCommentsInCommentsList(List<Comment> commentsToDisplay)
    {
        List<CommentControl> commentControls = new();

        foreach (Comment comment in commentsToDisplay)
        {
            CommentControl commentItem = new()
            {
                CommentId = comment.Id,
                UserName = string.Empty,
                Text = comment.Text ?? string.Empty,
                ReplyesList = new(),
                Likes = comment.Likes,
                Liked = false,
                CanBeEdited = false,
                IsCommentsOpened = false,
                DataContext = this
            };
            if (comment.UserId == UserService.SavedUser.Id) commentItem.CanBeEdited = true;
            commentControls.Add(commentItem);
        }  
        return commentControls;
    }

    private async void UpdateCommentsInformation(List<Comment> commentsToUpdateAsync)
    {
        foreach (Comment comment in commentsToUpdateAsync)
        {
            User? authorUser = await _userService.FindUserById(comment.UserId);
            if (authorUser is null) throw new ArgumentNullException(nameof(authorUser));
            CommentControl? foundedComment = CommentsToShow.FirstOrDefault(c => c.CommentId == comment.Id);
            if (foundedComment is null) throw new ArgumentNullException(nameof(foundedComment));
            foundedComment.UserName = authorUser.Name;
            if (UserService.SavedUser.Name == authorUser.Name) foundedComment.CanBeEdited = true;
        }
    }
}
