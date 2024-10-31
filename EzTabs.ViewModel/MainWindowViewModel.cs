using CommunityToolkit.Mvvm.Input;
using EzTabs.Services.NavigationServices;
using EzTabs.ViewModel.AuthControlsViewModels;
using EzTabs.ViewModel.MainControlsViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EzTabs.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly NavigationService _navigationService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindowViewModel()
        {
            _navigationService = NavigationService.Instance;

            _navigationService.NavigateTo(new SearchControlViewModel());

            _navigationService.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        public object CurrentViewModel => _navigationService.CurrentViewModel;

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
