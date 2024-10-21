using CommunityToolkit.Mvvm.Input;
using EzTabs.Services.ModelServices;
using EzTabs.Services.NavigationServices;
using EzTabs.ViewModel.BaseViewModels;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using EzTabs.Services.ValidationServices.CustomAttributes;

namespace EzTabs.ViewModel.AuthControlsViewModels
{
    public class RegistrationControlViewModel : BaseViewModel
    {
        private UserService? _userService;

        private string? _name;
        private string? _email;
        private string? _password;
        private string? _confirmPassword;

        [Required(ErrorMessage = "Username is required")]
        [MaxLength(20, ErrorMessage = "Username length can't be more than 20 characters")]
        [MinLength(2, ErrorMessage = "Username length can't be less than 2 charecters")]
        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        [Required(ErrorMessage = "Username is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(20, ErrorMessage = "Password length can't be more than 20 characters")]
        [MinLength(4, ErrorMessage = "Password length can't be less than 4 characters")]
        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        [Required(ErrorMessage = "Confirming password is required")]
        [EqualTo("Password", ErrorMessage = "Passwords aren't same")]
        public string? ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public ICommand RegisterCommand { get; }
        public ICommand GoToLoginCommand { get; }
        public ICommand GoToVerificationCommand { get; }

        public RegistrationControlViewModel()
        {
            RegisterCommand = new RelayCommand(async () => await Register());
            GoToLoginCommand = new RelayCommand(GoToLogin);
            GoToVerificationCommand = new RelayCommand(GoToVerification);
            _userService = new UserService();
        }

        private async Task Register()
        {
            Validate();
            if (HasErrors) return;

            if (_userService is null) throw new ArgumentNullException(nameof(_userService));
            if (_name is null || _email is null || _password is null) throw new NullReferenceException("Some of user data is missing");
            string verificationCode = Guid.NewGuid().ToString();
            await _userService.RegisterUser(_name,_email,_password, verificationCode);
            if(GoToVerificationCommand.CanExecute(null)) GoToVerificationCommand.Execute(null);
        }

        private static void GoToLogin()
        {
            NavigationService.Instance.NavigateTo(new LoginControlViewModel());
        }
        private static void GoToVerification()
        {
            NavigationService.Instance.NavigateTo(new VerificationControlViewModel());
        }
    }   
}
