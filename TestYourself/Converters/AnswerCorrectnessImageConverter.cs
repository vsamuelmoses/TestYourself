﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace TestYourself.Converters
{
	public class AnswerCorrectnessImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((bool) value)
				return "../Images/TickMarkpng.png";

			return "../Images/imageWrong.png";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
