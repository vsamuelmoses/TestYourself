using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TestYourself.Model;

namespace TestYourself.Converters
{
	public class ImageVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || string.IsNullOrEmpty(((ImageData)value).Path))
				return Visibility.Collapsed;

			return Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
