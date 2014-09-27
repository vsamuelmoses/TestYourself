using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TestYourself.Model;
using TestYourself.ViewModels;

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

	public class StampVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var state = (VmQuestionContent.States) value;

			if (state == (VmQuestionContent.States)Enum.Parse(typeof(VmQuestionContent.States), (string)parameter))
				return Visibility.Visible;

			return Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
