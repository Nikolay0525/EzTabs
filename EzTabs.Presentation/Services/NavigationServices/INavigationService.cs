using EzTabs.Presentation.ViewModels.BaseViewModels;

namespace EzTabs.Presentation.Services.NavigationServices;

public interface INavigationService
{
    void NavigateTo<T>() where T : BaseViewModel;
    BaseViewModel CurrentViewModel { get; }
}
