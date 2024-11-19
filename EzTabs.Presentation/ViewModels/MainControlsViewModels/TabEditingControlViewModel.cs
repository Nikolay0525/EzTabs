using CommunityToolkit.Mvvm.Input;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using EzTabs.Presentation.ViewModels.MainControlsViewModels.SimpleControlsViewModels.ControlBarPartsVMs;
using MySqlConnector.Logging;
using System.Windows.Input;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels;

public class TabEditingControlViewModel : BaseViewModel
{
    public BaseViewModel ControlBarViewModel { get; private set; }

    private bool _wholeNoteToggle = false;
    private bool _halfNoteToggle = false;
    private bool _quarterNoteToggle = false;
    private bool _eighthNoteToggle = false;
    private bool _sixteenthNoteToggle = false;
    private bool _thirtySecondNoteToggle = false;
    private string _currentFret;

    public bool WholeNoteToggle
    {
        get => _wholeNoteToggle;
        set
        {
            _wholeNoteToggle = value;
            OnPropertyChanged();
        }
    }

    public bool HalfNoteToggle
    {
        get => _halfNoteToggle;
        set
        {
            _halfNoteToggle = value;
            OnPropertyChanged();
        }
    }
    
    public bool QuarterNoteToggle
    {
        get => _quarterNoteToggle;
        set
        {
            _quarterNoteToggle = value;
            OnPropertyChanged();
        }
    }
    
    public bool EighthNoteToggle
    {
        get => _eighthNoteToggle;
        set
        {
            _eighthNoteToggle = value;
            OnPropertyChanged();
        }
    }
    
    public bool SixteenthNoteToggle
    {
        get => _sixteenthNoteToggle;
        set
        {
            _sixteenthNoteToggle = value;
            OnPropertyChanged();
        }
    }
    
    public bool ThirtySecondNoteToggle
    {
        get => _thirtySecondNoteToggle;
        set
        {
            _thirtySecondNoteToggle = value;
            OnPropertyChanged();
        }
    }
    
    public string CurrentFret
    {
        get => _currentFret;
        set
        {
            _currentFret = value;
            OnPropertyChanged();
        }
    }

    public TabEditingControlViewModel(IViewModelService viewModelService, INavigationService navigationService) : base(viewModelService, navigationService)
    {
        ControlBarViewModel = ViewModelService.CreateViewModel<ControlBarViewModel>();
    }

}
