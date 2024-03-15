using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhotoView.Desktop.Extensions;
using PhotoView.Desktop.Models;
using PhotoView.Desktop.Services;

namespace PhotoView.Desktop.ViewModels;

public partial class MainWindowViewModel : INotifyPropertyChanged
{
	private readonly IImageService _imageService;

	public MainWindowViewModel(IImageService imageService)
	{
		_imageService = imageService;
		LoadImagesCommand = new AsyncRelayCommand(LoadImagesAsync);
	}

	public ObservableCollection<ImageModel> Images { get; } = new();
	public AsyncRelayCommand LoadImagesCommand { get; }

	public async Task LoadImagesAsync()
	{
		var pageNumber = Random.Shared.Next(1, 50);
		var images = await _imageService.GetImagesAsync(pageNumber, 20);
		Images.Clear();
		foreach (var imageInfo in images)
		{
			var img = new Uri(imageInfo.Url).GetImage();
			Images.Add(new ImageModel
			{
				ImageSource = img,
				Name = imageInfo.Author
			});
		}
	}
}