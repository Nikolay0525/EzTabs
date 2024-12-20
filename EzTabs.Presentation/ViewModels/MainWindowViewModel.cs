﻿using CommunityToolkit.Mvvm.Input;
using EzTabs.Data.Domain.BaseModels;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.Services.ViewServices;
using EzTabs.Presentation.ViewModels.AuthControlsViewModels;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using EzTabs.Presentation.ViewModels.MainControlsViewModels;
using EzTabs.Presentation.Views.AuthControls;
using EzTabs.Presentation.Views.SimpleWindows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EzTabs.Presentation.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    private double _blurRadius = 0;

    public double BlurRadius
    {
        get => _blurRadius;
        set
        {
            _blurRadius = value;
            OnPropertyChanged(nameof(BlurRadius));
        }
    }

    public MainWindowViewModel(INavigationService navigationService, IViewModelService viewModelService, IWindowService windowService) : base(viewModelService, navigationService, windowService)
    {
        NavigationService = navigationService;
        ViewModelService = viewModelService;
        NavigationService.NavigateTo<LoginControlViewModel>();
        ViewModelService.OnSomethingLoadingChanged += OnSomethingLoadingChanged;
    }

    private void OnSomethingLoadingChanged()
    {
        BlurRadius = ViewModelService.SomethingLoading ? 40 : 0;
    }
}
