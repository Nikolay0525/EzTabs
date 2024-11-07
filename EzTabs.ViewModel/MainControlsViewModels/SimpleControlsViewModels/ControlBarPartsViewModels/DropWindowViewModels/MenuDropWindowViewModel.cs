using CommunityToolkit.Mvvm.Input;
using EzTabs.Model;
using EzTabs.Services.ModelServices;
using EzTabs.Services.NavigationServices;
using EzTabs.ViewModel.AuthControlsViewModels;
using EzTabs.ViewModel.BaseViewModels;
using System.Windows.Input;

namespace EzTabs.ViewModel.MainControlsViewModels.SimpleControlsViewModels.ControlBarPartsViewModels.DropWindowViewModels
{
    public class MenuDropWindowViewModel : BaseViewModel
    {
        private UserService _userService;

        private bool _showModerationButton = true;
        public bool ShowModerationButton
        {
            get => _showModerationButton;
            set
            {
                _showModerationButton = value;
                OnPropertyChanged(nameof(ShowModerationButton));
            }
        }

        public ICommand SignOutCommand { get; }

        public MenuDropWindowViewModel()
        {
            _userService = new UserService();
            SignOutCommand = new RelayCommand(SignOut);
            if (UserService.SavedUser is null) throw new ArgumentNullException(nameof(UserService.SavedUser));
            if (UserService.SavedUser.Role == Model.Enums.UserRole.User) ShowModerationButton = false;
        }

        private void SignOut()
        {
            NavigationService.Instance.NavigateTo(new LoginControlViewModel());
        }
    }
}
