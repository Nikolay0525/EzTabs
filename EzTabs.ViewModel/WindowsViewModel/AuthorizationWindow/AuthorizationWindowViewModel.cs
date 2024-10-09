using EzTabs.ViewModel.WindowsViewModel.AuthorizationWindow.UserControls;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;

namespace EzTabs.ViewModel.WindowsViewModel.AuthorizationWindow
{
    public class AuthorizationWindowViewModel : INotifyPropertyChanged
    {
        private object? _currentView;
        public object? CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand NavigatePagesCommand { get; }

        public AuthorizationWindowViewModel()
        {
            CurrentView = new LoginControlViewModel();
            NavigatePagesCommand = new RelayCommand<object>(async (viewModelName) => await NavigatePages(viewModelName));
        }

        public async Task NavigatePages(object viewModelName)
        {
            switch (viewModelName.ToString())
            {
                case "Login":
                    CurrentView = new LoginControlViewModel();
                    break;
                case "Registration":
                    CurrentView = new RegistrationControlViewModel();
                    break;
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
