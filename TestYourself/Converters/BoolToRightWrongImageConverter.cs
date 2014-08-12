using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TestYourself.Converters
{
    public class BoolToRightWrongImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "../Images/imageCorrect.png" : "../Images/imageWrong.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class GrayBulbVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter.ToString() == value.ToString() ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
