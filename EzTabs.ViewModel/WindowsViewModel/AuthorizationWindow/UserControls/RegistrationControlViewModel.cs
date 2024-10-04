using EzTabs.Model.Model;
using EzTabs.Model.Repository;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;


namespace EzTabs.ViewModel.WindowsViewModel.AuthorizationWindow.UserControls
{
    public class RegistrationControlViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<string, string> ShowMessage;

        private readonly RepoImplementation<User> _userRepository;
        private string _name;
        private string _email;
        private string _password;
        private string _confirmPassword;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public ICommand RegisterCommand { get; }

        public RegistrationControlViewModel()
        {
            // Parameterless constructor for XAML compatibility
        }

        public RegistrationControlViewModel(RepoImplementation<User> userRepository)
        {
            // Initialize the RegisterCommand
            RegisterCommand = new RelayCommand(async () => await Register());
            _userRepository = userRepository;
        }

        private async Task Register()
        {
            #region validation
            if (this.Name.Length < 2) 
            { 
                ShowMessage?.Invoke("Username too short, write atleast 2 symbols","Validation error");
                return;
            }
            if (this.Name.Length > 20) 
            { 
                ShowMessage?.Invoke("Username too long, it must be maximum 20 symbols","Validation error");
                return;
            }
            if (this.Password.Length < 8)
            {
                ShowMessage?.Invoke("Password too short, write atleast 8 symbols","Validation error");
                return;
            }
            if (this.Password.Length > 32)
            {
                ShowMessage?.Invoke("Password too long, it must be maximum 32 symbols","Validation error");
                return;
            }
            if (!this.Email.Contains('@')) 
            {
                ShowMessage?.Invoke("Your email is wrong, there are no @ symbol", "Validation error");
                return;
            }
            if(Password != ConfirmPassword)
            {
                ShowMessage?.Invoke("Your passwords don't match","Validation error");
                return;
            }
            #endregion
            var newUser = new User
            {
                Name = this.Name,
                Email = this.Email,
                Password = this.Password,
            };

            await _userRepository.Add(newUser);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
