namespace EzTabs.Services.NavigationServices
{
    public interface INavigationService
    {
        void NavigateTo<T>(T viewModel) where T : Enum;
        object? CurrentViewModel { get; set; }
        event Action? CurrentViewModelChanged;
    }
}
