using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using TestYourself.Model;

namespace TestYourself.Converters
{
	public class QuestionToMarkNumberOfAnswersTextConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var question = (Question) value;
			var numberOfCorrectAnswers = question.Answers.Count(answer => answer.IsCorrect);

			if (numberOfCorrectAnswers > 1)
				return string.Format("Mark {0} answers", numberOfCorrectAnswers);

			return string.Empty;

		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class MarkNumerOfAnswersVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var question = (Question)value;
			var numberOfCorrectAnswers = question.Answers.Count(answer => answer.IsCorrect);

			if (numberOfCorrectAnswers > 1)
				return Visibility.Visible;

			return Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
