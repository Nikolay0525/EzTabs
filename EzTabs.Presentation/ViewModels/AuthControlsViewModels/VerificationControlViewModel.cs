﻿using CommunityToolkit.Mvvm.Input;
using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.Services.NavigationServices;
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

        public ICommand TryToVerificateCommand { get; }
        public ICommand GoToRegistrationCommand { get; }

        public VerificationControlViewModel()
        {
            TryToVerificateCommand = new RelayCommand(async () => await TryToVerificate());
            GoToRegistrationCommand = new RelayCommand(GoToRegistration);
            _userService = new UserService();
        }

        private async Task TryToVerificate()
        {
            Validate();
            if (HasErrors) return;
            if (_verificationCode is null) throw new ArgumentNullException(nameof(_verificationCode));
            var isVerificated = await _userService.VerificateUser(_verificationCode);
            if (isVerificated)
            {
                NavigationService.Instance.NavigateTo(new LoginControlViewModel());
            }
            else { OnShowMessage("Validation", "Wrong verification code"); }
        }

        private void GoToRegistration()
        {
            OnShowOkCancelMessage("Warning", "If you not verify your account will be deleted, are you sure?");
            if (_userConfirm)
            {
                NavigationService.Instance.NavigateTo(new RegistrationControlViewModel());
            }
        }
    }
}