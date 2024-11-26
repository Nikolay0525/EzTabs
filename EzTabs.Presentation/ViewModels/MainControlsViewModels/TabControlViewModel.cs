using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using EzTabs.Presentation.ViewModels.MainControlsViewModels.SimpleControlsViewModels.ControlBarPartsVMs;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels;

public class TabControlViewModel : BaseViewModel
{
    public BaseViewModel ControlBarViewModel { get; private set; }

    private string _tabText;
    private string _titleBand;

    public string TabText
    {
        get => _tabText;
        private set
        {
            _tabText = value;
            OnPropertyChanged();
        }
    }
    
    public string TitleBand
    {
        get => _titleBand;
        private set
        {
            _titleBand = value;
            OnPropertyChanged();
        }
    }

    public TabControlViewModel(IViewModelService viewModelService, INavigationService navigationService)  : base(viewModelService, navigationService)
    {
        ControlBarViewModel = viewModelService.CreateViewModel<ControlBarViewModel>();
        TitleBand = TabService.SavedTab!.Title + TabService.SavedTab!.Band;
        TabText = TabService.SavedTab!.TabText;
    }
}
