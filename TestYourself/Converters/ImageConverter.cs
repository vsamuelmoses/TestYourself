using System;
using System.Globalization;
using System.Windows.Data;
using TestYourself.Model;

namespace TestYourself.Converters
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null) 
                return null;

            return ("../Databases/DrivingTheory/" + value);

        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
