using EzTabs.ViewModel.BaseViewModels;

namespace EzTabs.ViewModel.ViewModelHelper
{
    public interface IViewModelFactory
    {
        T CreateViewModel<T, U>(U? option) where T : BaseViewModel where U : class;
    }
}