using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Microsoft.Xna.Framework;
using TestYourself.ViewModels;

namespace TestYourself.Converters
{
	public class QuestionStateToImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var questionState = (VmQuestionContent.States)value;
			switch (questionState)
			{
				case VmQuestionContent.States.AnsweredCorrectly:
					return "../Images/iconBulbGreen.png";
				case VmQuestionContent.States.AnsweredInCorrectly:
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

	public class QuestionResultToColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var questionContent = (VmTestQuestionContent) value;

			if (questionContent.State == VmTestQuestionContent.States.AnsweredCorrectly)
				return new SolidColorBrush(Colors.Green);
			else if (questionContent.State == VmTestQuestionContent.States.AnsweredInCorrectly)
				return new SolidColorBrush(Colors.Red);
			else
				return new SolidColorBrush(Colors.Gray);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
