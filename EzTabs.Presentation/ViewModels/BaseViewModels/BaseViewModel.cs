using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ValidationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.Services.ViewServices;
using EzTabs.Presentation.ViewModels.AuthControlsViewModels;
using EzTabs.Presentation.Views.MainControls.SimpleControls.ControlBarParts.DropControls;
using EzTabs.Presentation.Views.SimpleWindows;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EzTabs.Presentation.ViewModels.BaseViewModels;

public abstract class BaseViewModel : ObservableObject ,INotifyPropertyChanged, INotifyDataErrorInfo
{
    private string _username;
    private List<ButtonInDropControl> _buttonsInMenu;

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged();
        }
    }

    public List<ButtonInDropControl> ButtonsInMenu
    {
        get => _buttonsInMenu;
        set
        {
            _buttonsInMenu = value;
            OnPropertyChanged();
        }
    }

    private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
    public bool HasErrors => _errors.Count > 0;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    private INavigationService _navigationService;
    private IViewModelService _viewModelService;
    private IWindowService _windowService;

    public INavigationService NavigationService 
    { 
        get => _navigationService; 
        set
        {
            _navigationService = value;
            OnPropertyChanged();
        }
    }
    public IViewModelService ViewModelService 
    { 
        get => _viewModelService; 
        set
        {
            _viewModelService = value;
            OnPropertyChanged();
        }
    }
    public IWindowService WindowService 
    { 
        get => _windowService; 
        set
        {
            _windowService = value;
            OnPropertyChanged();
        }
    }

    public ICommand SignOutCommand { get; }
    
    public static void ShowMessage(string title, string text)
    {
        OkMessage message = new(title,text);
        message.ShowDialog();
    }

    public static void ShowInfoMessage(string title, string text)
    {
        OkInfoMessage message = new(title,text);
        message.ShowDialog();
    }
    public static bool ShowOkCancelMessage(string title, string text)
    {
        OkCancelMessage message = new(title, text);
        bool? resultNull = message.ShowDialog();

        return resultNull ?? false;

    }
    public static bool ShowOkNoMessage(string title, string text)
    {
        OkNoMessage message = new(title, text);
        bool? resultNull = message.ShowDialog();

        return resultNull ?? false;

    }
    public static string ShowConfirmMessage(string title, string text)
    {
        ConfirmMessage message = new(title, text);
        bool? resultNull = message.ShowDialog();
        if (resultNull == true) return message.UserInputTextBox.Text;
        return string.Empty;
    }

    public void Validate(IEnumerable<string>? SpecificProperties = null)
    {
        _errors = ValidationService.ValidateProperties(this, SpecificProperties);
        foreach (var error in _errors)
        {
            OnErrorsChanged(error.Key);
        }

        if (HasErrors)
        {
            var errorMessage = new StringBuilder("Please correct the following errors:\n");
            foreach (var error in _errors)
            {
                errorMessage.AppendLine($"{error.Key}: {string.Join(", ", error.Value)}");
            }

            ShowMessage("Validation Error", errorMessage.ToString());
        }
    }

    public IEnumerable GetErrors(string? propertyName)
    {
        if (!string.IsNullOrEmpty(propertyName) && _errors.TryGetValue(propertyName, out List<string>? value))
        {
            return value;
        }
        return null;
    }

    public void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    public BaseViewModel(IViewModelService viewModelService, INavigationService navigationService, IWindowService windowService)
    {
        ViewModelService = viewModelService;
        NavigationService = navigationService;
        WindowService = windowService;

        if (UserService.SavedUser != null) Username = UserService.SavedUser.Name;
        SignOutCommand = new RelayCommand(SignOut);

        ButtonsInMenu = new()
        {
            new ButtonInDropControl()
            {
                IsButtonVisible = true,
                Text = "Sign Out",
                ButtonCommand = SignOutCommand
            }
        };
    }

    protected virtual void SignOut()
    {
        NavigationService.NavigateTo<LoginControlViewModel>();
    }
}

