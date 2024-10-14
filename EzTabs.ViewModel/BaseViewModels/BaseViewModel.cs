using EzTabs.Services.ValidationServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EzTabs.ViewModel.BaseViewModels;
public abstract class BaseViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
{
    private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

    public bool HasErrors => _errors.Count > 0;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
    public event Action<string, string>? ShowMessage;

    protected void OnShowMessage(string title, string message)
    {
        ShowMessage?.Invoke(title, message);
    }
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    protected void Validate()
    {
        _errors = ValidationService.Validate(this);
        foreach(var error in _errors)
        {
            OnErrorsChanged(error.Key);
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

    protected void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}
