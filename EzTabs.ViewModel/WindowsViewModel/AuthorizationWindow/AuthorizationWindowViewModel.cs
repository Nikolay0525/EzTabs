using EzTabs.ViewModel.WindowsViewModel.AuthorizationWindow.UserControls;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;

namespace EzTabs.ViewModel.WindowsViewModel.AuthorizationWindow
{
    public class AuthorizationWindowViewModel : INotifyPropertyChanged
    {
        private bool? _signInButtonVisibility;
        public bool? SignInButtonVisibility
        {
            get => _signInButtonVisibility;
            set
            {
                _signInButtonVisibility = value;
                OnPropertyChanged(nameof(SignInButtonVisibility));
            }
        }
        private bool? _signUpButtonVisibility;
        public bool? SignUpButtonVisibility
        {
            get => _signUpButtonVisibility;
            set
            {
                _signUpButtonVisibility = value;
                OnPropertyChanged(nameof(SignUpButtonVisibility));
            }
        }
        private bool? _goBackButtonVisibility;
        public bool? GoBackButtonVisibility
        {
            get => _goBackButtonVisibility;
            set
            {
                _goBackButtonVisibility = value;
                OnPropertyChanged(nameof(GoBackButtonVisibility));
            }
        }

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
            SignUpButtonVisibility = true;
            SignInButtonVisibility = false;
            GoBackButtonVisibility = false;
            CurrentView = new LoginControlViewModel();
            NavigatePagesCommand = new RelayCommand<object>(async (viewModelName) => await NavigatePages(viewModelName));
        }

        public async Task NavigatePages(object viewModelName)
        {
            switch (viewModelName.ToString())
            {
                case "Login":
                    CurrentView = new LoginControlViewModel();
                    SignUpButtonVisibility = true;
                    SignInButtonVisibility = false;
                    GoBackButtonVisibility = false;
                    break;
                case "Registration":
                    CurrentView = new RegistrationControlViewModel(new Services.ModelServices.UserService(), this);
                    SignUpButtonVisibility = false;
                    SignInButtonVisibility = true;
                    GoBackButtonVisibility = false;
                    break;
                case "Verification":
                    CurrentView = new VerificationControlViewModel();
                    SignInButtonVisibility = false;
                    SignUpButtonVisibility = false;
                    GoBackButtonVisibility = true;
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
