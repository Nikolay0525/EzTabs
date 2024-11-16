using CommunityToolkit.Mvvm.Input;
using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using EzTabs.Presentation.ViewModels.MainControlsViewModels.SimpleControlsViewModels.ControlBarPartsVMs.DropControlVMs;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Windows.Input;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels.SimpleControlsViewModels.ControlBarPartsVMs
{
    public class ControlBarViewModel : BaseViewModel
    {
        private bool _isMenuOpen = false;
        private string? _username;
        private BaseViewModel _menuDropControlViewModel;
        public BaseViewModel MenuDropControlViewModel
        {
            get => _menuDropControlViewModel;
            set
            {
                _menuDropControlViewModel = value;
                OnPropertyChanged();
            }
        }
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
        public ICommand SwitchMenuCommand { get; }
        public ControlBarViewModel(INavigationService navigationService,IViewModelService viewModelService) : base(viewModelService, navigationService)
        {
            MenuDropControlViewModel = viewModelService.CreateViewModel<MenuDropControlViewModel>();
            SwitchMenuCommand = new RelayCommand(SwitchMenu);
            if (UserService.SavedUser is null) throw new ArgumentNullException(nameof(UserService.SavedUser));
            Username = UserService.SavedUser.Name;
        }

        private void SwitchMenu()
        {
            IsMenuOpen = !IsMenuOpen;
        }
    }
}
