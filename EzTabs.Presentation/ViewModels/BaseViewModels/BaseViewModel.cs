using CommunityToolkit.Mvvm.ComponentModel;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ValidationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace EzTabs.Presentation.ViewModels.BaseViewModels;

public abstract class BaseViewModel : ObservableObject ,INotifyPropertyChanged, INotifyDataErrorInfo
{
    private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
    public bool HasErrors => _errors.Count > 0;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    private INavigationService _navigationService;
    private IViewModelService _viewModelService;
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

    public void ShowMessage(string title, string message)
    {
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
    }
    public void ShowOkCancelMessage(string title, string message)
    {
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
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

    public BaseViewModel(IViewModelService viewModelService, INavigationService navigationService)
    {
        ViewModelService = viewModelService;
        NavigationService = navigationService;
    }
}

