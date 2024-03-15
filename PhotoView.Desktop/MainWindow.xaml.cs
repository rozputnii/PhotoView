using System.Windows;
using PhotoView.Desktop.ViewModels;

namespace PhotoView.Desktop;

public partial class MainWindow : Window
{
	public MainWindow(MainWindowViewModel viewModel)
	{
		InitializeComponent();
		DataContext = viewModel;
	}
}