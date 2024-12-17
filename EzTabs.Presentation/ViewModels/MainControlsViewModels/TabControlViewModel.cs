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

using EzTabs.Presentation.Views.MainControls.SimpleControls;
using EzTabs.Presentation.Views.MainControls.SimpleControls.ControlBarParts.DropControls;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static EzTabs.Presentation.Views.MainControls.SimpleControls.CommentControl;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels;

public class TabControlViewModel : BaseViewModel
{

    private UserService _userService;
    private SearchingService _searchingService;
    private CommentService _commentService;
    private CommentRateService _commentRateService;
    private TabService _tabService;
    private TabRateService _tabRateService;

    private Guid _parentCommentId = Guid.Empty;

    private int _currentPage = 0;
    private bool _nextPageEnabled = false;
    private bool _previousPageEnabled = false;
    private bool _firstPageVisibility = false;
    private bool _isPageButtonsVisible = true;

    private bool _isZoomOpen = false;
    private bool _isInfoOpen = false;
    private bool _isSortByOpen = false;

    private bool _isCommentSectionVisible = false;
    private bool _isMessageWritingVisible = false;
    
    private bool _isRateOpen = false;
    private bool _imageButtonRateVisible = true;
    private bool _dropButtonRateVisible = false;
    private bool _oneStarCheck = false;
    private bool _twoStarCheck = false;
    private bool _threeStarCheck = false;
    private bool _fourStarCheck = false;
    private bool _fiveStarCheck = false;

    private int _zoom = 100;
    private double _fontSize = 33.3;
    private string _tabText;
    private string _titleBand;
    private string _writedMessage;

    private string _title;
    private string _band;
    private string _authorName;
    private string _genre;
    private string _key;
    private string _bpm;
    private string _description;
    private string _views;
    private string _rate;

    private ObservableCollection<ComboButtonControl> _listOfSortByOptions = new();
    private List<Comment> _listOfCommentsToShow = new();
    private List<CommentControl> _commentsToShow = new();
    private string _selectedOrderByOptionText = "Order By";
    private SortByOption _selectedOrderByOption = 0;

    private bool _isCommentsLoading = true;
    private double _commentsBlurRadius = 20;

    private Dictionary<string, SortByOption> SortByOptions { get; } = new()
    {
        { "Most Popular", SortByOption.Rating},
        { "Newest", SortByOption.Newest }
    };

    public bool IsCommentsLoading
    {
        get => _isCommentsLoading;
        set
        {
            _isCommentsLoading = value;
            CommentsBlurRadius = _isCommentsLoading ? 20 : 0;
            OnPropertyChanged();
        }
    }

    public double CommentsBlurRadius
    {
        get => _commentsBlurRadius;
        set
        {
            _commentsBlurRadius = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<ComboButtonControl> ListOfSortByOptions 
    { 
        get => _listOfSortByOptions;
        set
        {
            _listOfSortByOptions = value;
            OnPropertyChanged();
        }
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

    public bool IsRateOpen
    { 
        get => _isRateOpen;
        set
        {
            _isRateOpen = value;
            if (value)
            {
                DropButtonRateVisible = true;
                ImageButtonRateVisible = false;
            }
            else 
            {
                DropButtonRateVisible = false;
                ImageButtonRateVisible = true;
            }
            OnPropertyChanged();
        }
    } 
    
    public bool ImageButtonRateVisible
    { 
        get => _imageButtonRateVisible; 
        private set
        {
            _imageButtonRateVisible = value;
            OnPropertyChanged();
        }
    } 
    
    public bool DropButtonRateVisible
    { 
        get => _dropButtonRateVisible;
        private set
        {
            _dropButtonRateVisible = value;
            OnPropertyChanged();
        }
    }
    
    public bool OneStarCheck
    { 
        get => _oneStarCheck; 
        set
        {
            _oneStarCheck = value;
            OnPropertyChanged();
        }
    }
    
    public bool TwoStarCheck
    { 
        get => _twoStarCheck; 
        set
        {
            _twoStarCheck = value;
            OnPropertyChanged();
        }
    }
    
    public bool ThreeStarCheck
    { 
        get => _threeStarCheck; 
        set
        {
            _threeStarCheck = value;
            OnPropertyChanged();
        }
    }
    
    public bool FourStarCheck
    { 
        get => _fourStarCheck;
        set
        {
            _fourStarCheck = value;
            OnPropertyChanged();
        }
    }
    
    public bool FiveStarCheck
    { 
        get => _fiveStarCheck; 
        set
        {
            _fiveStarCheck = value;
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
    public string Views 
    { 
        get => _views;
        set
        {
            _views = value;
            OnPropertyChanged();
        }
    }
    public string Rate 
    { 
        get => _rate;
        set
        {
            _rate = value;
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

    public string WritedMessage 
    { 
        get => _writedMessage;
        set
        {
            _writedMessage = value;
            OnPropertyChanged();
        }
    }
    
    public string MessageType 
    { 
        get => _writedMessage;
        private set
        {
            _writedMessage = value;
            OnPropertyChanged();
        }
    }

    public ICommand ExportInFileCommand { get; }
    public ICommand OpenTabRateCommand { get; }
    public ICommand RateTabCommand { get; }
    public ICommand SubmitCommentCommand { get; }
    public ICommand WriteCommentCommand { get; }
    public ICommand WriteReplyOnCommentCommand { get; }
    public ICommand OpenReplyesOfCommentCommand { get; }
    public ICommand IncreaseReplyesAmountCommand { get; }
    public ICommand CancelWritingOfCommentCommand { get; }
    public ICommand SendLikeStateCommand { get; }
    public ICommand NextPageCommand { get; }
    public ICommand PreviousPageCommand { get; }
    public ICommand OnTheFirstPageCommand { get; }
    public ICommand GoToSearchCommand { get; }
    public ICommand HandleSortByCommand { get; }
    public ICommand ShowSortByOptionsCommand { get; }

    public TabControlViewModel(IViewModelService viewModelService, INavigationService navigationService, IWindowService windowService, 
        UserService userService, SearchingService searchingService, CommentRateService commentRateService, CommentService commentService, TabService tabService ,TabRateService tabRateService)  : base(viewModelService, navigationService, windowService)
    {
        _userService = userService;
        _searchingService = searchingService;
        _commentRateService = commentRateService;
        _commentService = commentService;
        _tabRateService = tabRateService;
        _tabService = tabService;
        ExportInFileCommand = new RelayCommand(ExportTab);
        OpenTabRateCommand = new AsyncRelayCommand(SetTabRate);
        RateTabCommand = new AsyncRelayCommand<string>(RateTab);
        SubmitCommentCommand = new AsyncRelayCommand(SubmitComment);
        SendLikeStateCommand = new AsyncRelayCommand<object>(SendLikeState);
        WriteCommentCommand = new RelayCommand(WriteComment);
        WriteReplyOnCommentCommand = new AsyncRelayCommand<Guid>(ReplyOnComment);
        OpenReplyesOfCommentCommand = new AsyncRelayCommand<object>(OpenCommentReplyes);
        IncreaseReplyesAmountCommand = new AsyncRelayCommand<Guid>(IncreaseReplyesAmount);
        CancelWritingOfCommentCommand = new RelayCommand(CancelWritingOfComment);
        NextPageCommand = new RelayCommand(NextPage);
        PreviousPageCommand = new RelayCommand(PreviousPage);
        OnTheFirstPageCommand = new RelayCommand(OnTheFirstPage);
        GoToSearchCommand = new RelayCommand(GoToSearchControl);
        HandleSortByCommand = new RelayCommand<string>(HandleSortBy);
        UpdateCommentsList();
        UpdateTabText();
        UpdateSortByList();
        Task.Run(UpdateTabInfoAsync);

        ButtonInDropControl exportTab = new()
        {
            IsButtonVisible = true,
            Text = "Export Tab",
            ButtonCommand = ExportInFileCommand
        };
        ButtonsInMenu.Insert(0, exportTab);
    }

    private void ExportTab()
    {
        try
        {
            string rootFolder = AppDomain.CurrentDomain.BaseDirectory;

            string targetFolder = Path.Combine(rootFolder, "ExportedTabs");

            Directory.CreateDirectory(targetFolder);

            var title = Title.Replace(" ", "_");
            var band = Band.Replace(" ", "_");

            string filePath = Path.Combine(targetFolder, $"{title}_{band}_By_{AuthorName}.txt");

            File.WriteAllText(filePath, TabText);

            ShowInfoMessage("Editor", $"Successfully exported in {filePath}");
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private async Task RateTab(string? rate)
    {
        try
        {
            if (!int.TryParse(rate, out int rateInt)) return;
            var allTabRates = await _tabRateService.ApplyTabRate(TabService.SavedTab!.Id, UserService.SavedUser.Id, rateInt);
            await _tabService.UpdateTabRating(TabService.SavedTab.Id, allTabRates.Item1);

            Action<bool>[] propertySetters =
            [
                value => OneStarCheck = value,
            value => TwoStarCheck = value,
            value => ThreeStarCheck = value,
            value => FourStarCheck = value,
            value => FiveStarCheck = value
            ];

            if (allTabRates.Item2) rateInt = 0;

            for (int i = 0; i < propertySetters.Length; i++)
            {
                propertySetters[i](i < rateInt);
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }
    
    private async Task SetTabRate()
    {
        try
        {
            List<TabRate> tabRates = await _tabRateService.GetAllTabRatesById(TabService.SavedTab!.Id);
            if (tabRates == null) return;
            TabRate? userTabRate = tabRates.Find(tr => tr.UserId == UserService.SavedUser.Id);
            if (userTabRate == null) return;

            Action<bool>[] propertySetters =
            [
                value => OneStarCheck = value,
            value => TwoStarCheck = value,
            value => ThreeStarCheck = value,
            value => FourStarCheck = value,
            value => FiveStarCheck = value
            ];

            for (int i = 0; i < propertySetters.Length; i++)
            {
                propertySetters[i](i < userTabRate.Rate);
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private async Task SubmitComment()
    {
        try
        {
            await _commentService.CreateComment(UserService.SavedUser.Id, TabService.SavedTab!.Id, WritedMessage, _parentCommentId);
            UpdateCommentsList();
            CancelWritingOfComment();
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }
    
    private void WriteComment()
    {
        IsMessageWritingVisible = true;
        MessageType = "New Message";
    }
    
    private async Task ReplyOnComment(Guid commentId)
    {
        try
        {
            IsMessageWritingVisible = true;
            _parentCommentId = commentId;
            var comment = await _commentService.FindCommentById(commentId);
            var user = await _userService.FindUserById(comment!.UserId);
            MessageType = "New Reply on @" + user!.Name + " comment";
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }
    
    private void CancelWritingOfComment()
    {
        IsMessageWritingVisible = false;
        _parentCommentId = Guid.Empty;
        WritedMessage = string.Empty;
        MessageType = string.Empty;
    }

    private async Task SendLikeState(object? parameter)
    {
        try
        {
            if (parameter is Tuple<Guid, bool> args) // where Item1 is id of comment and Item2 is state of UI element
            {
                int likes = await _commentRateService.ApplyCommentRate(UserService.SavedUser.Id, args.Item1, args.Item2);
                await _commentService.UpdateAmountOfLikesById(args.Item1, likes);

                var commentControl = _commentsToShow.Find(cc => cc.CommentId == args.Item1);

                commentControl ??= SearchForComment(args.Item1, CommentsToShow);
                if (commentControl == null) return;

                if (args.Item2)
                {
                    commentControl.Likes++;
                }
                else if (!args.Item2)
                {
                    commentControl.Likes--;
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
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
        FirstPageVisibility = false;
    }

    private void UpdateSortByList()
    {
        try
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
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private void HandleSortBy(string? selectedOption)
    {
        try
        {
            if (selectedOption is null) throw new ArgumentNullException(nameof(selectedOption));
            SelectedOrderByOption = selectedOption;
            IsSortByOpen = false;
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
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
        Views = TabService.SavedTab!.Views.ToString();
        Rate = TabService.SavedTab!.Rating.ToString();
        TabText = TabService.SavedTab!.TabText;
    }

    private async Task UpdateTabInfoAsync()
    {
        try
        {
            var foundedUser = await _userService.FindUserById(TabService.SavedTab!.AuthorId);
            if (foundedUser is null) return;
            AuthorName = foundedUser.Name;
            TitleBand = TabService.SavedTab!.Title + " - " + TabService.SavedTab!.Band + " by " + "@" + foundedUser.Name;
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private async void UpdateCommentsList()
    {
        try
        {
            IsCommentsLoading = true;
            var commentsToDisplay = await _searchingService.SearchComments((int)(WindowService.WindowHeight / 270), _currentPage, _selectedOrderByOption);

            if (commentsToDisplay.Item2 > 0)
            {
                NextPageEnabled = true;
            }
            else { NextPageEnabled = false; }
            CommentsToShow.Clear();
            CommentsToShow = await AddCommentsInCommentsList(commentsToDisplay.Item1, parentGuidCheck => parentGuidCheck == Guid.Empty);
            CommentsToShow = await UpdateCommentsInformation(_commentsToShow);
            IsCommentsLoading = false;
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private async Task<List<CommentControl>> AddCommentsInCommentsList(List<Comment> commentsToDisplay, Func<Guid, bool> condition)
    {
        try
        {
            List<CommentControl> commentControls = new();
            foreach (Comment comment in commentsToDisplay)
            {
                if (!condition(comment.ParentCommentId)) continue;
                CommentControl commentItem = new()
                {
                    AuthorId = comment.UserId,
                    CommentId = comment.Id,
                    ParentCommentId = comment.ParentCommentId,
                    UserName = string.Empty,
                    Text = comment.Text ?? string.Empty,
                    ReplyesList = new(),
                    Likes = comment.Likes,
                    Liked = false,
                    DateOfCreation = comment.DateOfCreation,
                    CanBeEdited = false,
                    IsReplyesOpened = false,
                    DataContext = this
                };

                if (comment.UserId == UserService.SavedUser.Id) commentItem.CanBeEdited = true;
                commentControls.Add(commentItem);

                bool liked = await _commentRateService.IsCommentLiked(UserService.SavedUser.Id, comment.Id);
                if (liked) commentItem.Liked = true;
            }
            return commentControls;
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
            return new();
        }
    }

    private async Task<List<CommentControl>> UpdateCommentsInformation(List<CommentControl> commentsToUpdate)
    {
        try
        {
            foreach (CommentControl comment in commentsToUpdate)
            {
                User? authorUser = await _userService.FindUserById(comment.AuthorId);
                if (authorUser is null) throw new ArgumentNullException(nameof(authorUser));
                comment.UserName = authorUser.Name;
                comment.ReplyesList = await _commentService.IsThereAnyReplyes(comment.CommentId, this);
            }
            return commentsToUpdate;
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
            return new();
        }
    }
    
    private async Task OpenCommentReplyes(object? parameter)
    {
        try
        {
            if (parameter is Tuple<Guid, bool> args && args.Item2) // where Item1 is id of comment and Item2 is state of UI element
            {
                await UpdateReplyesById(false, args.Item1);
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private async Task UpdateReplyesById(bool increaseAmount, Guid commentId)
    {
        try
        {
            CommentControl? updatingCommentControl = SearchForComment(commentId, CommentsToShow);
            if (updatingCommentControl is null) return;
            updatingCommentControl.IsLoading = true;

            if (increaseAmount) updatingCommentControl.ReplyesToShow *= 2;

            (List<Comment>, int) allReplyesOnComment = await _searchingService.SearchCommentReplyes(updatingCommentControl.ReplyesToShow, commentId);
            if (allReplyesOnComment.Item2 == 0) return;
            if (allReplyesOnComment.Item2 > updatingCommentControl.ReplyesToShow) updatingCommentControl.ShowMoreVisibile = true;
            else { updatingCommentControl.ShowMoreVisibile = false; }

            List<CommentControl> replyesControls = await AddCommentsInCommentsList(allReplyesOnComment.Item1, parentGuidCheck => parentGuidCheck != Guid.Empty);
            replyesControls = await UpdateCommentsInformation(replyesControls);

            replyesControls = replyesControls.OrderBy(rc => rc.DateOfCreation).ToList();

            updatingCommentControl.ReplyesList = replyesControls;
            updatingCommentControl.IsLoading = false;
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private async Task IncreaseReplyesAmount(Guid commentId)
    {
        try
        {
            await UpdateReplyesById(true, commentId);
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private CommentControl? SearchForComment(Guid commentId, List<CommentControl> comments)
    {
        foreach (var comment in comments)
        {
            if (comment.CommentId == commentId)
                return comment;

            var result = SearchForComment(commentId,comment.ReplyesList);
            if (result != null)
                return result;
        }

        return null; 
    }
}
