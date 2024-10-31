namespace EzTabs.Services.NavigationServices
{
    public interface INavigationService
    {
        void NavigateTo(object viewModel);
        object? CurrentViewModel { get; set; }
        event Action? CurrentViewModelChanged;
    }
}
