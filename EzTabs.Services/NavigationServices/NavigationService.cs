using System.Reflection;

namespace EzTabs.Services.NavigationServices
{
    public class NavigationService : INavigationService
    {
        private static NavigationService? _instance;
        public static NavigationService Instance => _instance ??= new NavigationService();

        public event Action? CurrentViewModelChanged;

        private object? _currentViewModel;
        public object? CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        public void NavigateTo(object viewModel)
        {
            CurrentViewModel = viewModel;
        }
        private NavigationService() { }
    }
}
