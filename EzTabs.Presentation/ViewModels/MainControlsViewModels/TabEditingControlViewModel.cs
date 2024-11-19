using CommunityToolkit.Mvvm.Input;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using MySqlConnector.Logging;
using System.Windows.Input;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels;

public class TabEditingControlViewModel : BaseViewModel
{

    public TabEditingControlViewModel(IViewModelService viewModelService, INavigationService navigationService) : base(viewModelService, navigationService)
    {
    }

}
