using CommunityToolkit.Mvvm.Input;
using EzTabs.Model;
using EzTabs.Services.ModelServices;
using EzTabs.Services.NavigationServices;
using EzTabs.Services.RepoServices;
using System.ComponentModel;
using System.Windows.Input;
using EzTabs.ViewModel.BaseViewModels;
using System.ComponentModel.DataAnnotations;

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
            Task.Run(InitializeAsync);
        }

        private async Task InitializeAsync()
        {
            var userRepo = await RepoInitializeService.InitializeRepoAsync<User>();
            _userService = new UserService(userRepo);
        }

        private async Task Register()
        {
            Validate();
            if (HasErrors) return;

            string verificationCode = Guid.NewGuid().ToString();
            var newUser = new User
            {
                Name = Name,
                Email = Email,
                Password = Password,
                VerificationCode = verificationCode
            };
            if (_userService is null) throw new ArgumentNullException(nameof(_userService));
            await _userService.RegisterUser(newUser, verificationCode);
            if(GoToVerificationCommand.CanExecute(null)) GoToVerificationCommand.Execute(null);
        }

        public static void GoToLogin()
        {
            NavigationService.Instance.NavigateTo(AuthViews.LoginControlViewModel);
        }
        public static void GoToVerification()
        {
            NavigationService.Instance.NavigateTo(AuthViews.VerificationControlViewModel);
        }
    }
}
