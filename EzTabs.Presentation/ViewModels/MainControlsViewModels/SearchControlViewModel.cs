using CommunityToolkit.Mvvm.Input;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using System.Windows.Input;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels;

public class SearchControlViewModel : BaseViewModel
{
    public ICommand GoToCreationOfTabCommand { get; }
    public SearchControlViewModel() : base()
    {
        GoToCreationOfTabCommand = new RelayCommand(GoToCreationOfTab);
    }

    private static void GoToCreationOfTab()
    {
        NavigationService.Instance.NavigateTo(new TabCreationControlViewModel());
    }
}
