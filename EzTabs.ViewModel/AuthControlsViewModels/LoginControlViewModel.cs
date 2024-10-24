﻿using System;
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
using EzTabs.ViewModel.MainControlsViewModels;

namespace EzTabs.ViewModel.AuthControlsViewModels
{
    public class LoginControlViewModel : BaseViewModel
    {
        public UserService _userService;

        private string? _username;
        private string? _password;

        public string? Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
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
            LoginCommand = new RelayCommand(async () => await Login());
            GoToRegistrationCommand = new RelayCommand(GoToRegistration);
            _userService = new UserService();
        }

        private async Task Login()
        {
            var userToLogin = new User
            {
                Name = Username,
                Password = Password
            };
            if (userToLogin is null) throw new ArgumentNullException(nameof(userToLogin));
            if (await _userService.LoginUser(userToLogin))
            {
                NavigationService.Instance.NavigateTo(new SearchControlViewModel());
            }
            else { OnShowMessage("Validation error", "User with such name and password not found"); }
        }

        private void GoToRegistration()
        {
            NavigationService.Instance.NavigateTo(AuthViews.RegistrationControlViewModel);
        }
    }
}