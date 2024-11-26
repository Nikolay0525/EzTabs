using CommunityToolkit.Mvvm.Input;
using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;


namespace EzTabs.Presentation.ViewModels.AuthControlsViewModels
{
    public class VerificationControlViewModel : BaseViewModel
    {
        private UserService _userService;

        private string? _verificationCode;
        private bool _userConfirm = false;

        [Required(ErrorMessage = "Verification code is required")]
        [MinLength(36, ErrorMessage = "Wrong type of code")]
        public string? VerificationCode
        {
            get => _verificationCode;
            set
            {
                _verificationCode = value;
                OnPropertyChanged(nameof(VerificationCode));
            }
        }

        public bool UserConfirm
        {
            get => _userConfirm;
            set
            {
                _userConfirm = value;
                OnPropertyChanged(nameof(UserConfirm));
            }
        }

        public ICommand VerificateCommand { get; }
        public ICommand GoToRegistrationCommand { get; }

        public VerificationControlViewModel(INavigationService navigationService, IViewModelService viewModelService, UserService userService) : base(viewModelService, navigationService)
        {
            VerificateCommand = new AsyncRelayCommand(TryToVerificate);
            GoToRegistrationCommand = new RelayCommand(GoToRegistration);
            _userService = userService;
        }

        private async Task TryToVerificate()
        {
            Validate();
            if (HasErrors) return;
            if (_verificationCode is null) throw new ArgumentNullException(nameof(_verificationCode));
            var isVerificated = await _userService.VerificateUser(_verificationCode);
            if (isVerificated)
            {
                NavigationService.NavigateTo<LoginControlViewModel>();
            }
            else { ShowMessage("Validation", "Wrong verification code"); }
        }

        private void GoToRegistration()
        {
            ShowOkCancelMessage("Warning", "If you not verify your account will be deleted, are you sure?");
            if (_userConfirm)
            {
                NavigationService.NavigateTo<RegistrationControlViewModel>();
            }
        }
    }
}
