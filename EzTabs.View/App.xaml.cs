using EzTabs.Services.ModelServices;
using EzTabs.Services.NavigationServices;
using EzTabs.View.Window;
using EzTabs.View.Window.AuthControls;
using EzTabs.ViewModel.AuthControlsViewModels;
using EzTabs.ViewModel.ViewModelHelper;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace EzTabs.View;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddTransient<UserService>();
                services.AddTransient<TabService>();
                services.AddTransient<TuningService>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var startupForm = AppHost.Services.GetRequiredService<EzTabs.View.Window.MainWindow>();
        startupForm.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}

