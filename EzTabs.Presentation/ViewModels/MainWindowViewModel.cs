using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.WindowServices;
using EzTabs.Presentation.ViewModels.AuthControlsViewModels;
using EzTabs.Presentation.ViewModels.BaseViewModels;

namespace EzTabs.Presentation.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    private readonly NavigationService _navigationService;
    private readonly WindowService _windowService;
    private double _blurRadius = 0;

    public MainWindowViewModel()
    {
        _navigationService = NavigationService.Instance;

        _navigationService.NavigateTo(new LoginControlViewModel());

        _navigationService.CurrentViewModelChanged += OnCurrentViewModelChanged;

        _windowService = WindowService.Instance;

        _windowService.SomethingLoadingChanged += OnSomethingLoadingChanged;
    }

    public object CurrentViewModel => _navigationService.CurrentViewModel;

    public bool SomethingLoading => _windowService.SomethingLoading;

    public double BlurRadius
    {
        get => _blurRadius;
        set
        {
            _blurRadius = value;
            OnPropertyChanged(nameof(BlurRadius));
        }
    }


    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    private void OnSomethingLoadingChanged()
    {
        OnPropertyChanged(nameof(SomethingLoading));
        BlurRadius = SomethingLoading ? 40 : 0;
    }
}
