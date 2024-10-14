
using System.Reflection;

namespace EzTabs.Services.NavigationServices
{
    public class NavigationService : INavigationService
    {
        private static NavigationService _instance;
        public static NavigationService Instance => _instance ??= new NavigationService();

        public event Action CurrentViewModelChanged;

        private object _currentViewModel;
        public object CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        public void NavigateTo<T>(T view) where T : Enum
        {
            string viewNamespace;
            if (view is AuthViews)
            {
                viewNamespace = "AuthControlsViewModels";
            }
            else if (view is MainViews)
            {
                viewNamespace = "MainControlsViewModels";
            }
            else throw new ArgumentNullException(nameof(viewNamespace));
            string viewString = view.ToString();
            if (viewString is null) throw new ArgumentNullException(nameof(viewString));
            Assembly assembly = Assembly.Load("EzTabs.ViewModel");
            if (assembly is null) throw new ArgumentNullException(nameof(assembly));
            Type? type = assembly.GetType($"EzTabs.ViewModel.{viewNamespace}.{viewString}");
            if (type is null) throw new ArgumentNullException(nameof(type));
            CurrentViewModel = Activator.CreateInstance(type);
        }
        private NavigationService() { }
    }
}
