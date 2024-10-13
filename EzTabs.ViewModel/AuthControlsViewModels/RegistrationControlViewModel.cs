using CommunityToolkit.Mvvm.Input;
using EzTabs.Model;
using EzTabs.Services.ModelServices;
using EzTabs.Services.NavigationServices;
using System.ComponentModel;
using System.Windows.Input;

namespace EzTabs.ViewModel.AuthControlsViewModels
{
    public class RegistrationControlViewModel : INotifyPropertyChanged
    {
        private UserService? _userService;

        private string? _name;
        private string? _email;
        private string? _password;
        private string? _confirmPassword;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event Action<string, string>? ShowMessage;

        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string? ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public ICommand? RegisterCommand { get; }

        public RegistrationControlViewModel() { }

        public RegistrationControlViewModel(UserService userService)
        {
            RegisterCommand = new RelayCommand(async () => await Register());
            _userService = userService;
        }

        private async Task Register()
        {
            #region validation
            if (Name == null)
            {
                ShowMessage?.Invoke("Username field is empty", "Validation error");
                return;
            }
            if (Name.Length < 2)
            {
                ShowMessage?.Invoke("Username too short, write atleast 2 symbols", "Validation error");
                return;
            }
            if (Name.Length > 20)
            {
                ShowMessage?.Invoke("Username too long, it must be maximum 20 symbols", "Validation error");
                return;
            }
            if (Password == null)
            {
                ShowMessage?.Invoke("Password field is empty", "Validation error");
                return;
            }
            if (Password.Length < 8)
            {
                ShowMessage?.Invoke("Password too short, write atleast 8 symbols", "Validation error");
                return;
            }
            if (Password.Length > 32)
            {
                ShowMessage?.Invoke("Password too long, it must be maximum 32 symbols", "Validation error");
                return;
            }
            if (Email == null)
            {
                ShowMessage?.Invoke("Email field is empty", "Validation error");
                return;
            }
            if (!Email.Contains('@'))
            {
                ShowMessage?.Invoke("Your email is wrong, there are no @ symbol", "Validation error");
                return;
            }
            if (Password != ConfirmPassword)
            {
                ShowMessage?.Invoke("Your passwords don't match", "Validation error");
                return;
            }
            #endregion
            string verificationCode = Guid.NewGuid().ToString();
            var newUser = new User
            {
                Name = Name,
                Email = Email,
                Password = Password,
                VerificationCode = verificationCode
            };

            NavigationService.Instance.NavigateTo(new LoginControlViewModel());
            await _userService.RegisterUser(newUser, verificationCode);

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
