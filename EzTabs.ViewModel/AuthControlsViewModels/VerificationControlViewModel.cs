using CommunityToolkit.Mvvm.Input;
using EzTabs.Model;
using EzTabs.Services.ModelServices;
using EzTabs.Services.RepoServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EzTabs.ViewModel.BaseViewModels;

namespace EzTabs.ViewModel.AuthControlsViewModels
{
    public class VerificationControlViewModel : BaseViewModel
    {
        private UserService? _userService = null;

        private string? _verificationCode;
        public string? VerificationCode
        {
            get => _verificationCode;
            set
            {
                _verificationCode = value;
                OnPropertyChanged(nameof(VerificationCode));
            }
        }

        public ICommand TryToVerificateCommand { get; }

        public VerificationControlViewModel()
        {
            TryToVerificateCommand = new RelayCommand(async () => await TryToVerificate());
            Task.Run(InitializeAsync);
        }
        private async Task<UserService> InitializeAsync()
        {
            var userRepo = await RepoInitializeService.InitializeRepoAsync<User>();
            return _userService = new UserService(userRepo);
        }

        private async Task TryToVerificate()
        {
            _userService.VerificateUser()
        }
    }
}
