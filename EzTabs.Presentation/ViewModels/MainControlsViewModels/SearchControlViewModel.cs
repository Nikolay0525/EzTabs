using CommunityToolkit.Mvvm.Input;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using EzTabs.Presentation.ViewModels.MainControlsViewModels.SimpleControlsViewModels.ControlBarPartsVMs;
using System.Windows.Input;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels;

public class SearchControlViewModel : BaseViewModel
{
    public ICommand GoToCreationOfTabCommand { get; }

    public BaseViewModel ControlBarViewModel { get; private set; }

    public SearchControlViewModel(INavigationService navigationService ,IViewModelService viewModelService) : base(viewModelService, navigationService)
    {
        ControlBarViewModel = ViewModelService.CreateViewModel<ControlBarViewModel>();
        GoToCreationOfTabCommand = new RelayCommand(GoToCreationOfTab);
    }

    private void GoToCreationOfTab()
    {
        NavigationService.NavigateTo<TabCreationControlViewModel>();
    }
}
