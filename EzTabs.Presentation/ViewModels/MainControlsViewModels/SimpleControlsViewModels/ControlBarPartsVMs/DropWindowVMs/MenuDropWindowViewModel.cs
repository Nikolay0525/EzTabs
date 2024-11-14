using CommunityToolkit.Mvvm.Input;
using EzTabs.Data.Domain.Enums;
using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.ViewModels.AuthControlsViewModels;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using System.Windows.Input;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels.SimpleControlsViewModels.ControlBarPartsVMs.DropWindowVMs
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
            if (UserService.SavedUser.Role == UserRole.User) ShowModerationButton = false;
        }

        private void SignOut()
        {
            NavigationService.Instance.NavigateTo(new LoginControlViewModel());
        }
    }
}
