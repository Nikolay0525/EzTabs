using CommunityToolkit.Mvvm.Input;
using EzTabs.Data.Domain.Enums;
using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.ViewModels.AuthControlsViewModels;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Windows.Input;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels.SimpleControlsViewModels.ControlBarPartsVMs
{
    public class ControlBarViewModel : BaseViewModel
    {
        private bool _isMenuOpen = false;
        private string? _username;
        private bool _showModerationButton = true;

        public bool IsMenuOpen
        {
            get => _isMenuOpen;
            set
            {
                _isMenuOpen = value;
                OnPropertyChanged(nameof(IsMenuOpen));
            }
        }
        public string? Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public bool ShowModerationButton
        {
            get => _showModerationButton;
            set
            {
                _showModerationButton = value;
                OnPropertyChanged(nameof(ShowModerationButton));
            }
        }

        public ICommand SwitchMenuCommand { get; }
        public ICommand CreateTabCommand { get; }
        public ICommand SignOutCommand { get; }

        public ControlBarViewModel(INavigationService navigationService,IViewModelService viewModelService) : base(viewModelService, navigationService)
        {
            SwitchMenuCommand = new RelayCommand(SwitchMenu);
            CreateTabCommand = new RelayCommand(CreateTab);
            SignOutCommand = new RelayCommand(SignOut);
            if (UserService.SavedUser is null) throw new ArgumentNullException(nameof(UserService.SavedUser));
            if (UserService.SavedUser.Role == UserRole.User) ShowModerationButton = false;
            Username = UserService.SavedUser.Name;
        }

        private void SwitchMenu()
        {
            IsMenuOpen = !IsMenuOpen;
        }

        private void CreateTab()
        {
            NavigationService.NavigateTo<TabCreationControlViewModel>();
        }
        
        private void SignOut()
        {
            NavigationService.NavigateTo<LoginControlViewModel>();
        }
    }
}
