using System;
using System.Globalization;
using System.Windows.Data;

namespace TestYourself.Converters
{
	public class ImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType,
			object parameter, CultureInfo culture)
		{
			if (value == null) 
				return null;

			if(((string)value).StartsWith("DrivingTheory"))
				return ("../Databases/" + value);
			else
				return ("../Databases/DrivingTheory/" + value);

		}

		public object ConvertBack(object value, Type targetType,
			object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
