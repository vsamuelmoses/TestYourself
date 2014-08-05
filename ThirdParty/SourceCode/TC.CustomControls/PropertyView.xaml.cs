using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Wallet;

namespace TC.CustomControls
{
	public partial class PropertyView : UserControl
	{
		public PropertyView()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
			"Title", typeof(string), typeof(PropertyView), new PropertyMetadata(default(string), OnPropertyChanged));

		private static void OnPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
		{
			var thisControl = (PropertyView)dependencyObject;
			thisControl.Refresh();
		}

		private void Refresh()
		{
			TitleTextBlock.Text = Title;
			ValueTextBlock.Text = Value;
		}

		public string Title
		{
			get { return (string) GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}

		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
			"Value", typeof(string), typeof(PropertyView), new PropertyMetadata(default(string), OnPropertyChanged));

		public string Value
		{
			get { return (string) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

	}
}
