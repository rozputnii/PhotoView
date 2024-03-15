using System.Net.Http;
using System.Text.Json;
using PhotoView.Desktop.Models;

namespace PhotoView.Desktop.Services.Implementation;

public sealed class ImageService(IHttpClientFactory httpClientFactory) : IImageService
{
	public async Task<IEnumerable<ImageInfo>> GetImagesAsync(int page, int limit, CancellationToken cancellationToken)
	{
		var query = $"images?page={page}&limit={limit}";

		var httpClient = httpClientFactory.CreateClient(HttpClientNames.ImageApiClient);
		var json = await httpClient.GetStringAsync(query, cancellationToken);

		var images = Deserialize(json);
		return images ?? Array.Empty<ImageInfo>();
	}


	private IEnumerable<ImageInfo>? Deserialize(string json)
	{
		var options = new JsonSerializerOptions(JsonSerializerOptions.Default)
		{
			PropertyNameCaseInsensitive = true
		};
		return JsonSerializer.Deserialize<IEnumerable<ImageInfo>?>(json, options);
	}
}