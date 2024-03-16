using System.Text.Json.Serialization;

namespace PhotoView.Domain.Images.Models;

internal sealed record PicsumApiImage
{
	public string? Id { get; init; }
	public string? Author { get; init; }
	public int Width { get; init; }
	public int Height { get; init; }
	public string? Url { get; init; }

	[JsonPropertyName("download_url")] public required string DownloadUrl { get; init; }
}