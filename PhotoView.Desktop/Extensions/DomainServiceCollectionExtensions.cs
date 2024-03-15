using System.Net.Http;
using Microsoft.Extensions.Configuration;
using PhotoView.Desktop.Handlers;
using PhotoView.Desktop.Models;
using PhotoView.Desktop.Services;
using PhotoView.Desktop.Services.Implementation;
using Polly;
using Polly.Extensions.Http;
namespace Microsoft.Extensions.DependencyInjection;

public static class DesktopServiceCollectionExtensions
{
	public static IServiceCollection AddDesktopServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<AuthConfig>(configuration);

		services
			.AddTransient<IImageService, ImageService>()
			.AddImageApiHttpClient(configuration)
			;


		return services;
	}

	public static IServiceCollection AddImageApiHttpClient(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient<ImageApiAuthHandler>();

		var retryPolicy = HttpPolicyExtensions
		   .HandleTransientHttpError()
		   .WaitAndRetryAsync(new[] { TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5), });

		var noOpPolicy = Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();

		// DnB
		services
			.AddHttpClient(HttpClientNames.ImageApiClient, client =>
			{
				var uri = configuration.GetValue<string>("ImagesApiUrl");
				ArgumentNullException.ThrowIfNull(uri);

				client.BaseAddress = new Uri(uri);
			})
			.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { AllowAutoRedirect = false })
			.AddHttpMessageHandler<ImageApiAuthHandler>()
			.AddPolicyHandler(request => request.Method == HttpMethod.Get ? retryPolicy : noOpPolicy)
			;

		return services;
	}
}