using System;
using System.Globalization;
using System.Windows.Data;
using TestYourself.Model;

namespace TestYourself.Converters
{
    public class QuestionToIndexConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var question = (Question) value;

            if (question != null)
                return "Question No.: " + question.Index + " / " + question.AssociatedTopic.Questions.Count;

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
