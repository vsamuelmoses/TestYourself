using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Networking.Voip;

namespace TC.CustomControls
{
	public partial class MenuItem : UserControl
	{
		public static readonly DependencyProperty ImagePathProperty = DependencyProperty.Register(
			"ImagePath", typeof(string), typeof(MenuItem), new PropertyMetadata(default(string), OnDependentPropertyChanged));

		public string ImagePath
		{
			get { return (string) GetValue(ImagePathProperty); }
			set { SetValue(ImagePathProperty, value); }
		}

		public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
			"Text", typeof (string), typeof (MenuItem), 
			new PropertyMetadata(default(string), OnDependentPropertyChanged));

		private static void OnDependentPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
		{
			var thisControl = (MenuItem) dependencyObject;
			thisControl.Refresh();
		}

		private void Refresh()
		{
			if (IsMonochrome)
			{
				MonochromeImage.Visibility = Visibility.Visible;
				NotMonoChromeImage.Visibility = Visibility.Collapsed;
			}
			else
			{
				MonochromeImage.Visibility = Visibility.Collapsed;
				NotMonoChromeImage.Visibility = Visibility.Visible;
			}

			if (!string.IsNullOrEmpty(ImagePath))
			{
				MonochromeImageBrush.ImageSource = new BitmapImage(new Uri(ImagePath, UriKind.RelativeOrAbsolute));
				NotMonoChromeImage.Source = new BitmapImage(new Uri(ImagePath, UriKind.Relative));
			}

			TitleTextBlock.FontFamily = TitleFontFamily;
			TitleTextBlock.FontSize = TitleFontSize;
			TitleTextBlock.Text = Text;
		}


		public string Text
		{
			get { return (string) GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public static readonly DependencyProperty TitleFontSizeProperty = DependencyProperty.Register(
			"TitleFontSize", typeof (double), typeof (MenuItem),
			new PropertyMetadata((double)Application.Current.Resources["PhoneFontSizeLarge"], OnDependentPropertyChanged));

		public double TitleFontSize
		{
			get { return (double) GetValue(TitleFontSizeProperty); }
			set { SetValue(TitleFontSizeProperty, value); }
		}

		public static readonly DependencyProperty TitleFontFamilyProperty = DependencyProperty.Register(
			"TitleFontFamily", typeof (FontFamily), typeof (MenuItem),
			new PropertyMetadata(Application.Current.Resources["PhoneFontFamilyLight"], OnDependentPropertyChanged));

		public FontFamily TitleFontFamily
		{
			get { return (FontFamily) GetValue(TitleFontFamilyProperty); }
			set { SetValue(TitleFontFamilyProperty, value); }
		}

		public static readonly DependencyProperty IsMonochromeProperty = DependencyProperty.Register(
			"IsMonochrome", typeof (bool), typeof (MenuItem), new PropertyMetadata(default(bool)));

		public bool IsMonochrome
		{
			get { return (bool) GetValue(IsMonochromeProperty); }
			set { SetValue(IsMonochromeProperty, value); }
		}
		
		public MenuItem()
		{
			InitializeComponent();
		}
	}
}
