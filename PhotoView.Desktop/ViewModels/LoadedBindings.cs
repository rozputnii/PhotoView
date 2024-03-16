using System.Windows;
using System.Windows.Input;

namespace PhotoView.Desktop.ViewModels;

public class LoadedBindings
{
	public static readonly DependencyProperty LoadedEnabledProperty =
		DependencyProperty.RegisterAttached(
			"LoadedEnabled",
			typeof(bool),
			typeof(LoadedBindings),
			new PropertyMetadata(false, OnLoadedEnabledPropertyChanged));


	public static readonly DependencyProperty LoadedCommandProperty =
		DependencyProperty.RegisterAttached(
			"LoadedCommand",
			typeof(ICommand),
			typeof(LoadedBindings),
			new PropertyMetadata(null));

	public static bool GetLoadedEnabled(DependencyObject sender)
	{
		return (bool)sender.GetValue(LoadedEnabledProperty);
	}

	public static void SetLoadedEnabled(DependencyObject sender, bool value)
	{
		sender.SetValue(LoadedEnabledProperty, value);
	}

	private static void OnLoadedEnabledPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is Window w)
		{
			var newEnabled = (bool)e.NewValue;
			var oldEnabled = (bool)e.OldValue;

			if (oldEnabled && !newEnabled)
				w.Loaded -= WindowLoaded;
			else if (!oldEnabled && newEnabled)
				w.Loaded += WindowLoaded;
		}
	}

	private static void WindowLoaded(object sender, RoutedEventArgs e)
	{
		var loadedAction = GetLoadedCommand((Window)sender);
		loadedAction?.Execute(sender);
	}

	public static ICommand GetLoadedCommand(DependencyObject sender)
	{
		return (ICommand)sender.GetValue(LoadedCommandProperty);
	}

	public static void SetLoadedCommand(DependencyObject sender, ICommand value)
	{
		sender.SetValue(LoadedCommandProperty, value);
	}
}