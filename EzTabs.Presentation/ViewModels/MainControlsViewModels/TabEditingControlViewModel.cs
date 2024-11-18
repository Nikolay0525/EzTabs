using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.ViewModels.BaseViewModels;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels;

public class TabEditingControlViewModel : BaseViewModel
{
    public TabEditingControlViewModel(IViewModelService viewModelService, INavigationService navigationService) : base(viewModelService, navigationService)
    {

    }
}
