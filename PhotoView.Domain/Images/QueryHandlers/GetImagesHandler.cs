using System.Text.Json;
using MediatR;
using PhotoView.Domain.Images.Models;
using PhotoView.Domain.Images.Queries;
using PhotoView.Domain.Models;

namespace PhotoView.Domain.Images.QueryHandlers;

public class GetImagesHandler
	(IHttpClientFactory httpClientFactory) : IRequestHandler<GetImages, IEnumerable<ImageInfo>>
{
	public async Task<IEnumerable<ImageInfo>> Handle(GetImages request, CancellationToken cancellationToken)
	{
		var query = $"list?page={request.Page}&limit={request.Limit}";

		var httpClient = httpClientFactory.CreateClient(HttpClientNames.PicsumApiClient);
		var json = await httpClient.GetStringAsync(query, cancellationToken);

		var images = Deserialize(json);
		if (images is null) return Array.Empty<ImageInfo>();

		var resultImages = images.Select(x => new ImageInfo { Id = x.Id, Author = x.Author, Url = x.DownloadUrl });

		return resultImages.ToList();
	}

	private IEnumerable<PicsumApiImage>? Deserialize(string json)
	{
		var options = new JsonSerializerOptions(JsonSerializerOptions.Default)
		{
			PropertyNameCaseInsensitive = true
		};
		return JsonSerializer.Deserialize<IEnumerable<PicsumApiImage>?>(json, options);
	}
}