﻿using CommunityToolkit.Mvvm.Input;
using EzTabs.Data.Domain;
using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.Services.ViewServices;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using EzTabs.Presentation.ViewModels.MainControlsViewModels;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace EzTabs.Presentation.ViewModels.AuthControlsViewModels;

public class LoginControlViewModel : BaseViewModel
{
    private readonly UserService _userService;

    private string _username;
    private string _password;

    [Required(ErrorMessage = "Username is required")]
    [MinLength(2, ErrorMessage = "Username is too short, username is atleast 2 symbols length")]
    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(2, ErrorMessage = "Password is too short, username is atleast 4 symbols length")]
    public string Password
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

    public LoginControlViewModel(INavigationService navigationService, IViewModelService viewModelService, IWindowService windowService, UserService userService) : base(viewModelService, navigationService, windowService)
    {
        _userService = userService;
        LoginCommand = new AsyncRelayCommand(Login);
        GoToRegistrationCommand = new RelayCommand(GoToRegistration);
    }

    private async Task Login()
    {
        try
        {
            Validate();
            if (HasErrors) return;
            ViewModelService.SomethingLoading = true;
            var userToLogin = new User
            {
                Name = Username,
                Password = Password
            };
            if (userToLogin is null) throw new ArgumentNullException(nameof(userToLogin));
            if (await _userService.LoginUser(userToLogin))
            {
                NavigationService.NavigateTo<SearchControlViewModel>();
                ViewModelService.SomethingLoading = false;
                return;
            }
            ViewModelService.SomethingLoading = false;
            ShowMessage("Validation error", "User with such name and password not found");
        }
        catch (Exception ex)
        {
            ViewModelService.SomethingLoading = false;
            ShowMessage("Exception", ex.Message);
        }
    }

    private void GoToRegistration()
    {
        NavigationService.NavigateTo<RegistrationControlViewModel>();
    }
}