using CommunityToolkit.Mvvm.Input;
using EzTabs.Services.ModelServices;
using EzTabs.ViewModel.BaseViewModels;
using System.Windows.Input;

namespace EzTabs.ViewModel.MainControlsViewModels.SimpleControlsViewModels.ControlBarPartsViewModels
{ 
    public class ControlBarViewModel : BaseViewModel
    {
        private bool _isMenuOpen = false;
        private string? _username;
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
        public ControlBarViewModel()
        {
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
