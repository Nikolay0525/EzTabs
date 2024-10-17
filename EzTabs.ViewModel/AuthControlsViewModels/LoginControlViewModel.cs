using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using EzTabs.Services.NavigationServices;
using EzTabs.Model;
using EzTabs.Services.ModelServices;
using EzTabs.Services.RepoServices;
using EzTabs.ViewModel.BaseViewModels;

namespace EzTabs.ViewModel.AuthControlsViewModels
{
    public class LoginControlViewModel : BaseViewModel
    {
        public UserService? _userService = null;

        private string? _name;
        private string? _password;

        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
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

        public ICommand? LoginCommand { get; }
        public ICommand? GoToRegistrationCommand { get; }

        public LoginControlViewModel()
        {
            GoToRegistrationCommand = new RelayCommand(GoToRegistration);
            _userService = new UserService();
        }

        private async Task Login()
        {
            #region validation

            #endregion

        }

        public void GoToRegistration()
        {
            NavigationService.Instance.NavigateTo(AuthViews.RegistrationControlViewModel);
        }
    }
}