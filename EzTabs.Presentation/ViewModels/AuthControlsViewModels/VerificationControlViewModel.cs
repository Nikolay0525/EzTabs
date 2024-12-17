using CommunityToolkit.Mvvm.Input;
using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.Services.ViewServices;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;


namespace EzTabs.Presentation.ViewModels.AuthControlsViewModels
{
    public class VerificationControlViewModel : BaseViewModel
    {
        private UserService _userService;

        private string? _verificationCode;

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
        public ICommand VerificateCommand { get; }
        public ICommand GoToRegistrationCommand { get; }

        public VerificationControlViewModel(INavigationService navigationService, IViewModelService viewModelService, IWindowService windowService, UserService userService) : base(viewModelService, navigationService, windowService)
        {
            VerificateCommand = new AsyncRelayCommand(TryToVerificate);
            GoToRegistrationCommand = new RelayCommand(GoToRegistration);
            _userService = userService;
        }

        private async Task TryToVerificate()
        {
            try
            {
                Validate();
                if (HasErrors) return;
                ViewModelService.SomethingLoading = true;
                if (_verificationCode is null) throw new ArgumentNullException(nameof(_verificationCode));
                var isVerificated = await _userService.VerificateUser(_verificationCode);
                if (isVerificated)
                {
                    NavigationService.NavigateTo<LoginControlViewModel>();
                }
                else { ShowMessage("Validation", "Wrong verification code"); }
                ViewModelService.SomethingLoading = false;
            }
            catch (Exception ex)
            {
                ViewModelService.SomethingLoading = false;
                ShowMessage("Exception", ex.Message);
            }
        }

        private void GoToRegistration()
        {
            if (ShowOkCancelMessage("Warning", "If you not verify your account will be deleted, are you sure?"))
            {
                NavigationService.NavigateTo<RegistrationControlViewModel>();
            }
        }
    }
}
