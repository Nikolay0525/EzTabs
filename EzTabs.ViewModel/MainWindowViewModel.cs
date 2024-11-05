using EzTabs.Services.NavigationServices;
using EzTabs.ViewModel.MainControlsViewModels;
using System.ComponentModel;

namespace EzTabs.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly NavigationService _navigationService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindowViewModel()
        {
            _navigationService = NavigationService.Instance;

            _navigationService.NavigateTo(new TabCreationControlViewModel());

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
