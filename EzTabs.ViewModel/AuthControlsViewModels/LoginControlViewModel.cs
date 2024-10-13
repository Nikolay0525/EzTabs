using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using EzTabs.Services.NavigationServices;

namespace EzTabs.ViewModel.AuthControlsViewModels
{
    public class LoginControlViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event Action<string, string>? ShowMessage;

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

        public ICommand? GoToRegistrationCommand { get; }

        public LoginControlViewModel()
        {
            GoToRegistrationCommand = new RelayCommand(GoToRegistration);
        }

        private async Task Login()
        {
            #region validation

            #endregion

        }

        public void GoToRegistration()
        {
            NavigationService.Instance.NavigateTo(new RegistrationControlViewModel());
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}