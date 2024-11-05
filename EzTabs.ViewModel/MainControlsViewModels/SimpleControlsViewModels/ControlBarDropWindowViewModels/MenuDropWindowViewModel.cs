using CommunityToolkit.Mvvm.Input;
using EzTabs.ViewModel.BaseViewModels;
using System.Windows.Input;

namespace EzTabs.ViewModel.MainControlsViewModels.SimpleControlsViewModels.ControlBarDropWindowViewModels
{
    public class MenuDropWindowViewModel : BaseViewModel 
    {
        public ICommand SignOutCommand { get; }

        public MenuDropWindowViewModel() 
        {
            SignOutCommand = new RelayCommand();
        }

        private void SignOut()
        {

        }
    }
}
