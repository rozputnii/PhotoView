using System.Net.Cache;
using System.Windows.Media.Imaging;

namespace PhotoView.Desktop.Extensions;

public static class UriExtensions
{
    public static BitmapImage GetImage(this Uri imageUri, Action? complete = null, Action? failed = null)
    {
        var imageSource = new BitmapImage();

		imageSource.DownloadCompleted += (sender, e) => complete?.Invoke();
        imageSource.DecodeFailed += (sender, e) => failed?.Invoke();
        imageSource.DownloadFailed += (sender, e) =>
        {
            failed?.Invoke();
            imageSource.Freeze();
        };
        
        imageSource.BeginInit();
        imageSource.UriSource = imageUri;
        imageSource.CacheOption = BitmapCacheOption.None;
        imageSource.UriCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
        imageSource.EndInit();

        if (imageSource.IsDownloading) return imageSource;

        imageSource.Freeze();
        complete?.Invoke();

        return imageSource;
    }
}