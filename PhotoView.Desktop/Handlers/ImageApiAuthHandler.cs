using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PhotoView.Desktop.Models;

namespace PhotoView.Desktop.Handlers;

public class ImageApiAuthHandler
	(ILogger<ImageApiAuthHandler> logger, IOptions<AuthConfig> authConfig) : DelegatingHandler
{
	protected override async Task<HttpResponseMessage> SendAsync(
		HttpRequestMessage request,
		CancellationToken cancellationToken
	)
	{
		logger.LogInformation("Retrieving Image Api Authorization info.");

		request.Headers.Add("x-svc-auth-key", authConfig.Value.ImagesApiAuthKey);
		return await base.SendAsync(request, cancellationToken);
	}
}