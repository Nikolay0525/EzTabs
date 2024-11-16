using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using System.Text;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.Services.ValidationServices.CustomAttributes;
using EzTabs.Presentation.Services.ViewModelServices;

namespace EzTabs.Presentation.ViewModels.AuthControlsViewModels;

public class RegistrationControlViewModel : BaseViewModel
{
    private UserService? _userService;

    private string? _username;
    private string? _email;
    private string? _password;
    private string? _confirmPassword;

    [Required(ErrorMessage = "Username is required")]
    [MinLength(2, ErrorMessage = "Username length can't be less than 2 charecters")]
    public string? Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    [Required(ErrorMessage = "Email is required")]
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

    public RegistrationControlViewModel(INavigationService navigationService, IViewModelService viewModelService, UserService userService) : base(viewModelService, navigationService)
    {
        _userService = userService;
        RegisterCommand = new AsyncRelayCommand(Register);
        GoToLoginCommand = new RelayCommand(GoToLogin);
    }

    private async Task Register()
    {
        Validate();
        if (HasErrors) return;

        if (_userService is null) throw new ArgumentNullException(nameof(_userService));
        if (_username is null || _email is null || _password is null) throw new NullReferenceException("Some of user data is missing");
        string verificationCode = Guid.NewGuid().ToString();
        List<string> errors = await _userService.RegisterUser(_username, _email, _password, verificationCode);
        #region validation
        if (errors.Count != 0)
        {
            var errorMessage = new StringBuilder("Please correct the following errors:\n");
            foreach (var error in errors)
            {
                errorMessage.AppendLine(error);
            }

            ShowMessage("Validation Error", errorMessage.ToString());
            return;
        }
        #endregion
        NavigationService.NavigateTo<VerificationControlViewModel>();
    }

    private void GoToLogin()
    {
        NavigationService.NavigateTo<LoginControlViewModel>();
    }
}
