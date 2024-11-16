using CommunityToolkit.Mvvm.ComponentModel;
using EzTabs.Presentation.ViewModels.BaseViewModels;

namespace EzTabs.Presentation.Services.NavigationServices;

public class NavigationService : ObservableObject, INavigationService
{
    private readonly Func<Type, BaseViewModel> _viewModelFactory;
    private BaseViewModel _currentViewModel;
    public BaseViewModel CurrentViewModel
    {
        get => _currentViewModel;
        private set
        { 
            _currentViewModel = value;
            OnPropertyChanged();
        }
    }

    public NavigationService(Func<Type, BaseViewModel> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public void NavigateTo<TViewModel>() where TViewModel : BaseViewModel
    {
        BaseViewModel baseViewModel = _viewModelFactory.Invoke(typeof(TViewModel));
        CurrentViewModel = baseViewModel;
    }
}
