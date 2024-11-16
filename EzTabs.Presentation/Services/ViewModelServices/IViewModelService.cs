using EzTabs.Presentation.ViewModels.BaseViewModels;

namespace EzTabs.Presentation.Services.ViewModelServices;

public interface IViewModelService
{
    bool SomethingLoading { get; set; }
    BaseViewModel CreateViewModel<TViewModel>() where TViewModel : BaseViewModel;
    event Action OnSomethingLoadingChanged;
}
