using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhotoView.Desktop.ViewModels;

namespace PhotoView.Desktop;

public partial class App : Application
{
	public IHost AppHost { get; } = Host.CreateDefaultBuilder()
		.ConfigureServices(ConfigureServices)
		.Build();
		
	protected override async void OnStartup(StartupEventArgs e)
	{
		await AppHost.StartAsync();
		var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
		mainWindow.Show();
		base.OnStartup(e);
	}

	private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
	{
		services.AddDesktopServices(context.Configuration);
		services.AddTransient<MainWindowViewModel>();
		services.AddSingleton<MainWindow>();
	}
}