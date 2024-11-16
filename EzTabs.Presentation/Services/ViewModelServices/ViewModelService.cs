using CommunityToolkit.Mvvm.ComponentModel;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using System.ComponentModel;

namespace EzTabs.Presentation.Services.ViewModelServices;

public class ViewModelService : ObservableObject, IViewModelService
{
    private readonly Func<Type, BaseViewModel> _viewModelFactory;
    private bool _somethingLoading = false;
    public event Action OnSomethingLoadingChanged;
    public bool SomethingLoading
    {
        get => _somethingLoading;
        set
        {
            _somethingLoading = value;
            OnPropertyChanged();
            OnSomethingLoadingChanged?.Invoke();
        }
    }
    public ViewModelService(Func<Type, BaseViewModel> viewModelFactory) 
    {
        _viewModelFactory = viewModelFactory;
    }

    public BaseViewModel CreateViewModel<TViewModel>() where TViewModel : BaseViewModel
    {
        BaseViewModel baseViewModel = _viewModelFactory.Invoke(typeof(TViewModel));
        return baseViewModel;
    }
}
