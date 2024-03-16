using Microsoft.Extensions.Configuration;
using PhotoView.Domain.Models;
using Polly;
using Polly.Extensions.Http;

namespace Microsoft.Extensions.DependencyInjection;

public static class DomainServiceCollectionExtensions
{
	public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
	{
		services
			.AddPicsumApiHttpClient(configuration)
			;


		return services;
	}

	public static IServiceCollection AddPicsumApiHttpClient(this IServiceCollection services,
		IConfiguration configuration)
	{
		var retryPolicy = HttpPolicyExtensions
			.HandleTransientHttpError()
			.WaitAndRetryAsync(new[] { TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5) });

		var noOpPolicy = Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();

		// DnB
		services
			.AddHttpClient(HttpClientNames.PicsumApiClient, client =>
			{
				var uri = configuration.GetValue<string>("ImagesApiUrl");
				ArgumentNullException.ThrowIfNull(uri);

				client.BaseAddress = new Uri(uri);
			})
			.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { AllowAutoRedirect = false })
			.AddPolicyHandler(request => request.Method == HttpMethod.Get ? retryPolicy : noOpPolicy)
			;

		return services;
	}
}