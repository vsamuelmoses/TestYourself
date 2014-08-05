using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TC.CustomControls
{
	public partial class MenuItem : UserControl
	{
		public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register(
			"ImageWidth", typeof (double), typeof (MenuItem), new PropertyMetadata(default(double)));

		public double ImageWidth
		{
			get { return (double) GetValue(ImageWidthProperty); }
			set { SetValue(ImageWidthProperty, value); }
		}

		public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register(
			"ImageHeight", typeof (double), typeof (MenuItem), new PropertyMetadata(default(double)));

		public double ImageHeight
		{
			get { return (double) GetValue(ImageHeightProperty); }
			set { SetValue(ImageHeightProperty, value); }
		}


		public static readonly DependencyProperty ImagePathProperty = DependencyProperty.Register(
			"ImagePath", typeof (string), typeof (MenuItem), new PropertyMetadata(default(string)));

		public string ImagePath
		{
			get { return (string) GetValue(ImagePathProperty); }
			set { SetValue(ImagePathProperty, value); }
		}

		public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
			"Text", typeof (string), typeof (MenuItem), new PropertyMetadata(default(string)));

		public string Text
		{
			get { return (string) GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public static readonly DependencyProperty TitleFontSizeProperty = DependencyProperty.Register(
			"TitleFontSize", typeof (double), typeof (MenuItem),
			new PropertyMetadata((double)Application.Current.Resources["PhoneFontSizeLarge"]));

		public double TitleFontSize
		{
			get { return (double) GetValue(TitleFontSizeProperty); }
			set { SetValue(TitleFontSizeProperty, value); }
		}

		public static readonly DependencyProperty TitleFontFamilyProperty = DependencyProperty.Register(
			"TitleFontFamily", typeof (FontFamily), typeof (MenuItem),
			new PropertyMetadata(Application.Current.Resources["PhoneFontFamilyLight"]));

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
			Loaded += OnMenuItemLoaded;
		}

		void OnMenuItemLoaded(object sender, RoutedEventArgs e)
		{
			DataContext = this;
		}
	}
}
