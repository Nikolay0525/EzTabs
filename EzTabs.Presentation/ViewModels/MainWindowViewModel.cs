using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.ViewModels.AuthControlsViewModels;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using EzTabs.Presentation.ViewModels.MainControlsViewModels;
using EzTabs.Presentation.Views.AuthControls;
using System.Windows.Controls;

namespace EzTabs.Presentation.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    private double _blurRadius = 0;

    public MainWindowViewModel(INavigationService navigationService, IViewModelService viewModelService) : base(viewModelService, navigationService)
    {
        NavigationService = navigationService;
        ViewModelService = viewModelService;
        NavigationService.NavigateTo<TabEditingControlViewModel>();
        ViewModelService.OnSomethingLoadingChanged += OnSomethingLoadingChanged;
    }

    public double BlurRadius
    {
        get => _blurRadius;
        set
        {
            _blurRadius = value;
            OnPropertyChanged(nameof(BlurRadius));
        }
    }

    private void OnSomethingLoadingChanged()
    {
        BlurRadius = ViewModelService.SomethingLoading ? 40 : 0;
    }
}
