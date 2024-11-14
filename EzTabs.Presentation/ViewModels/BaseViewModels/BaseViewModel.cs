using EzTabs.Presentation.Services.ValidationServices;
using System.Collections;
using System.ComponentModel;
using System.Text;

namespace EzTabs.Presentation.ViewModels.BaseViewModels;

public abstract class BaseViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
{
    private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
    public bool HasErrors => _errors.Count > 0;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
    public event Action<string, string>? ShowMessage;
    public event Action<string, string>? ShowOkCancelMessage;

    public void OnShowMessage(string title, string message)
    {
        ShowMessage?.Invoke(title, message);
    }
    public void OnShowOkCancelMessage(string title, string message)
    {
        ShowOkCancelMessage?.Invoke(title, message);
    }
    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

            OnShowMessage("Validation Error", errorMessage.ToString());
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
}

