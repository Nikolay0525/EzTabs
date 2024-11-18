using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.ViewModels;
using EzTabs.Presentation.ViewModels.AuthControlsViewModels;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using EzTabs.Presentation.ViewModels.MainControlsViewModels;
using EzTabs.Presentation.ViewModels.MainControlsViewModels.SimpleControlsViewModels.ControlBarPartsVMs;
using EzTabs.Presentation.ViewModels.MainControlsViewModels.SimpleControlsViewModels.ControlBarPartsVMs.DropControlVMs;
using EzTabs.Presentation.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace EzTabs.Presentation;

public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddSingleton<MainWindow>(serviceProvider => new Views.MainWindow
        {
            DataContext = serviceProvider.GetRequiredService<MainWindowViewModel>()
        });

        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient<LoginControlViewModel>();
        services.AddTransient<RegistrationControlViewModel>();
        services.AddTransient<VerificationControlViewModel>();
        services.AddTransient<SearchControlViewModel>();
        services.AddTransient<TabControlViewModel>();
        services.AddTransient<TabCreationControlViewModel>();
        services.AddTransient<TabEditingControlViewModel>();
        services.AddTransient<ControlBarViewModel>();
        services.AddTransient<VerificationControlViewModel>();
        services.AddTransient<UserService>();
        services.AddTransient<TabService>();
        services.AddTransient<TuningService>();

        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IViewModelService, ViewModelService>();
        services.AddSingleton<Func<Type, BaseViewModel>>(serviceProvider => viewModelType => (BaseViewModel)serviceProvider.GetRequiredService(viewModelType)); // ViewModelFactory

        _serviceProvider = services.BuildServiceProvider();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        if (mainWindow is null) throw new ArgumentNullException(nameof(mainWindow));
        mainWindow.Show();

        base.OnStartup(e);
    }
}

