using System;
using System.Globalization;
using System.Windows.Data;
using TestYourself.ViewModels;

namespace TestYourself.Converters
{
    public class QuestionStateToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var questionState = (VmQuestion.States)value;
            switch (questionState)
            {
                case VmQuestion.States.AnsweredCorrectly:
                    return "../Images/iconBulbGreen.png";
                case VmQuestion.States.AnsweredInCorrectly:
                    return "../Images/iconBulbRed.png";
                default :
                    return "../Images/iconBulb.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
