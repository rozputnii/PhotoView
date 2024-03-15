using System.Net;
using Microsoft.Extensions.Options;

namespace PhotoView.Api.Authorization;

public class ApiKeyMiddleware(RequestDelegate next, IOptions<AuthConfig> authConfig)
{
	private readonly AuthConfig _authConfig = authConfig.Value;

	public async Task InvokeAsync(HttpContext context)
	{
		if (!context.Request.Headers.TryGetValue("x-svc-auth-key", out var apiKey) || apiKey != _authConfig.AuthKey)
		{
			context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
			return;
		}

		await next(context);
	}
}