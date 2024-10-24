using EzTabs.ViewModel.BaseViewModels;
using EzTabs.Services.NavigationServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace EzTabs.ViewModel.MainControlsViewModels
{
    public class SearchControlViewModel : BaseMainControlViewModel
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
}
