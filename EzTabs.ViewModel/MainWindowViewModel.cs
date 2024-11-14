using EzTabs.NavigationServices;
using EzTabs.Services.WindowServices;
using EzTabs.ViewModel.AuthControlsViewModels;
using EzTabs.ViewModel.BaseViewModels;

namespace EzTabs.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private readonly WindowService _windowService;
        private double _blurRadius = 0;

        public MainWindowViewModel()
        {
            _navigationService = NavigationService.Instance;

            _navigationService.NavigateTo(new LoginControlViewModel());

            _navigationService.CurrentViewModelChanged += OnCurrentViewModelChanged;

            _windowService = WindowService.Instance;

            _windowService.SomethingLoadingChanged += OnSomethingLoadingChanged;
        }

        public object CurrentViewModel => _navigationService.CurrentViewModel;

        public bool SomethingLoading => _windowService.SomethingLoading;

        public double BlurRadius
        {
            get => _blurRadius;
            set
            {
                _blurRadius = value;
                OnPropertyChanged(nameof(BlurRadius));
            }
        }


        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void OnSomethingLoadingChanged()
        {
            OnPropertyChanged(nameof(SomethingLoading));
            BlurRadius = SomethingLoading ? 40 : 0;
        }
    }
}
