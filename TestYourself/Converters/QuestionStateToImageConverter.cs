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
}
