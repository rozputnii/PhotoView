namespace PhotoView;

public sealed record ImageInfo
{
	public string? Id { get; init; }
	public string? Author { get; init; }
	public required string Url { get; init; }
}