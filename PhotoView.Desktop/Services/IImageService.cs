namespace PhotoView.Desktop.Services;

public interface IImageService
{
	Task<IEnumerable<ImageInfo>> GetImagesAsync(int page, int limit, CancellationToken cancellationToken = default);
}