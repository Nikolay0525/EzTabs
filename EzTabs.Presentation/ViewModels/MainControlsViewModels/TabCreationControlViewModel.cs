using CommunityToolkit.Mvvm.Input;
using EzTabs.Data.Domain;
using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ValidationServices.CustomAttributes;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using EzTabs.Presentation.ViewModels.MainControlsViewModels.SimpleControlsViewModels.ControlBarPartsVMs;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using System.Windows.Navigation;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels;

public class TabCreationControlViewModel : BaseViewModel
{
    public BaseViewModel ControlBarViewModel { get; private set; }

    private readonly TabService _tabService;
    private readonly TuningService _tuningService;

    private bool _addVisibilitySwitch = true;
    private bool _editRemoveVisibilitySwitch = false;
    private string _selectedItem;
    private string _title;
    private string _band;
    private string _genre;
    private string _key;
    private int _bpm = 120;
    private string _description;
    private int _stringOrder = 1;
    private string _stringNote = "e";
    private ObservableCollection<Tuning> _tunings = new();
    private ObservableCollection<string> _listOfTunings = new();

    public bool AddVisibilitySwitch
    {
        get => _addVisibilitySwitch;
        set
        {
            _addVisibilitySwitch = value;
            OnPropertyChanged(nameof(AddVisibilitySwitch));
        }
    }
    public bool EditRemoveVisibilitySwitch
    {
        get => _editRemoveVisibilitySwitch;
        set
        {
            _editRemoveVisibilitySwitch = value;
            OnPropertyChanged(nameof(EditRemoveVisibilitySwitch));
        }
    }

    public string SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            OnPropertyChanged(nameof(SelectedItem));
            OnSelecting();
        }
    }

    [Required(ErrorMessage = "Title is required")]
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    [Required(ErrorMessage = "Band is required")]
    public string Band
    {
        get => _band;
        set
        {
            _band = value;
            OnPropertyChanged(nameof(Band));
        }
    }
    [Required(ErrorMessage = "Genre is required")]
    [AllowedCharacters(@"^[a-zA-Z-]+$", ErrorMessage = "Only letters and \"-\" symbol can be written in genre")]
    public string Genre
    {
        get => _genre;
        set
        {
            _genre = value;
            OnPropertyChanged(nameof(Genre));
        }
    }
    [Required(ErrorMessage = "Key is required")]
    [AllowedCharacters(@"^[a-zA-Z#1-7]+$", ErrorMessage = "Only letters of notes and \"#\" symbol can be written in Key")]
    public string Key
    {
        get => _key;
        set
        {
            _key = value;
            OnPropertyChanged(nameof(Key));
        }
    }
    public int BitsPerMinute
    {
        get => _bpm;
        set
        {
            _bpm = value;
            OnPropertyChanged(nameof(BitsPerMinute));
            OnPropertyChanged(nameof(BitsPerMinuteText));
        }
    }
    public string BitsPerMinuteText => $"{BitsPerMinute} BPM";

    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }

    [Required(ErrorMessage = "String Order is required")]
    [AllowedCharacters(@"^[1-8]+$", ErrorMessage = "Only number 1-8 can be set as String Order")]
    public int StringOrder
    {
        get => _stringOrder;
        set
        {
            _stringOrder = value;
            OnPropertyChanged(nameof(StringOrder));
            ManageButtonAccessibility();
        }
    }

    [Required(ErrorMessage = "String Note is required")]
    [AllowedCharacters(@"^[cCdDeEfFgGaAbB#]+$", ErrorMessage = "Only notes letters and \"#\" symbol can be written in String Note")]
    public string StringNote
    {
        get => _stringNote;
        set
        {
            _stringNote = value;
            OnPropertyChanged(nameof(StringNote));
        }
    }

    public ObservableCollection<string> ListOfTunings
    {
        get => _listOfTunings;
        set => _listOfTunings = value;
    }

    public ICommand GoToSearchControlCommand { get; }
    public ICommand CreateTabCommand { get; }
    public ICommand AddTuningCommand { get; }
    public ICommand EditTuningCommand { get; }
    public ICommand RemoveTuningCommand { get; }

    public TabCreationControlViewModel(INavigationService navigationService, IViewModelService viewModelService, TabService tabService, TuningService tuningService) : base(viewModelService, navigationService)
    {
        _tabService = tabService;
        _tuningService = tuningService;
        ControlBarViewModel = ViewModelService.CreateViewModel<ControlBarViewModel>();
        CreateTabCommand = new AsyncRelayCommand(CreateTab);
        GoToSearchControlCommand = new RelayCommand(GoToSearchControl);
        AddTuningCommand = new RelayCommand(AddTuning);
        EditTuningCommand = new RelayCommand(EditTuning);
        RemoveTuningCommand = new RelayCommand(RemoveTuning);
    }

    private void GoToSearchControl()
    {
        NavigationService.NavigateTo<SearchControlViewModel>();
    }

    private void ManageButtonAccessibility()
    {
        if (_tunings is null) throw new ArgumentNullException(nameof(_tunings));
        if (_tunings.FirstOrDefault(t => t.StringOrder == StringOrder) == null)
        {
            AddVisibilitySwitch = true;
            EditRemoveVisibilitySwitch = false;
        }
        else
        {
            AddVisibilitySwitch = false;
            EditRemoveVisibilitySwitch = true;
        }
    }

    private void OnSelecting()
    {
        if (SelectedItem is null) return;
        var selectedItem = SelectedItem.Split(":");
        if (int.TryParse(selectedItem[0], out int result) != default)
        {
            StringOrder = result;
            StringNote = selectedItem[1];
        }
        else { throw new ArgumentException(nameof(result) + "is default"); }
    }

    private void AddTuning()
    {
        List<string> SpecificProperties = new()
        {
            nameof(StringOrder),
            nameof(StringNote)
        };
        Validate(SpecificProperties);
        if (HasErrors) return;

        var stringNote = StringNote;

        if (StringNote.Length < 2) stringNote = stringNote + " ";

        if (_tunings.Any(t => t.StringOrder == StringOrder))
        {
            ShowMessage("Validation", "You can't add two same order strings");
            return;
        }
        var newTuning = new Tuning
        {
            StringOrder = StringOrder,
            StringNote = stringNote
        };
        _tunings.Add(newTuning);
        ListUpdater(_tunings);
        ManageButtonAccessibility();
    }

    private void EditTuning()
    {
        if (_tunings is null) throw new ArgumentNullException(nameof(_tunings));
        if (ListOfTunings is null) throw new ArgumentNullException(nameof(_tunings));
        if (StringNote is null) throw new ArgumentNullException(nameof(StringNote));

        var editingTuning = _tunings.FirstOrDefault(t => t.StringOrder == StringOrder);

        if (editingTuning != null)
        {
            editingTuning.StringNote = StringNote;
            ListUpdater(_tunings);
        }
    }

    private void RemoveTuning()
    {
        if (_tunings is null) throw new ArgumentNullException(nameof(_tunings));
        if (ListOfTunings is null) throw new ArgumentNullException(nameof(_tunings));

        var removingTuning = _tunings.FirstOrDefault(t => t.StringOrder == StringOrder);

        if (removingTuning != null)
        {
            _tunings.Remove(removingTuning);
            ListUpdater(_tunings);
        }
        ManageButtonAccessibility();
    }

    private void ListUpdater(ObservableCollection<Tuning> tunings)
    {
        ListOfTunings.Clear();
        foreach (var tuning in tunings.OrderBy(t => t.StringOrder))
        {
            ListOfTunings.Add($"{tuning.StringOrder}:{tuning.StringNote}");
            OnPropertyChanged(nameof(ListOfTunings));
        }
    }

    private async Task CreateTab()
    {
        Validate();
        if (HasErrors) return;
        if (_tunings is null) throw new ArgumentNullException(nameof(_tunings));
        var tuningsList = _tunings.ToList();
        if (Title is null || Band is null || Genre is null || Key is null || Description is null) throw new ArgumentNullException("There are null reference in Title or Band or Genre or Key or Description properties");
        Tab? createdTab = await _tabService.CreateTab(UserService.SavedUser.Id, Title, Band, Genre, Key, BitsPerMinute, Description);
        #region validation
        if (createdTab is null)
        {
            ShowMessage("Validation Error", "You can't create your own two absolute same tablature, change Title, Band, or Genre");
            return;
        }
        #endregion
        await _tuningService.CreateTuning(createdTab, tuningsList);
        NavigationService.NavigateTo<TabEditingControlViewModel>();
    }
}
