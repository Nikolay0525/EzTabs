namespace EzTabs.Presentation.Services.WindowServices;

public interface IWindowService
{
    bool SomethingLoading { get; set; }
    event Action? SomethingLoadingChanged;
}
