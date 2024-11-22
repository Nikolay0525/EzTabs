using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.ViewModels.BaseViewModels;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels;

public class TabControlViewModel : BaseViewModel
{
    public TabControlViewModel(IViewModelService viewModelService, INavigationService navigationService)  : base(viewModelService, navigationService)
    {

    }
}
