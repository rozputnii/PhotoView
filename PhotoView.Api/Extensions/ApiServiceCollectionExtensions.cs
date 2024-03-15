using PhotoView.Api.Authorization;

namespace Microsoft.Extensions.DependencyInjection;

public static class ApiServiceCollectionExtensions
{
	public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<AuthConfig>(configuration);

		services
			.AddDomainServices(configuration)
			.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DomainServiceCollectionExtensions).Assembly));
		;

		return services;
	}
}