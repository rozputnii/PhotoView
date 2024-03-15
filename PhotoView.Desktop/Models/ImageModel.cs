using System.Windows.Media;

namespace PhotoView.Desktop.Models;

public class ImageModel
{
    public required ImageSource ImageSource { get; init; }
    public string? Name { get; init; }
}