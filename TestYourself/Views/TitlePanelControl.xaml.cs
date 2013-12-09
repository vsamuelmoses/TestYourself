using System.Windows;
using System.Windows.Controls;

namespace TestYourself.Views
{
	public partial class TitlePanelControl : UserControl
	{
		public TitlePanelControl()
		{
			// Required to initialize variables
			InitializeComponent();
		}
		
		public void OnTitleChanged(string oldTitle, string newTitle)
		{
			PageTitleTextBlock.Text = newTitle;
		}

		public void OnPercentageChanged(double oldPercentage, double newPercentage)
		{
			PercentageProgressBar.Value= newPercentage;
		}

		private static void OnTitleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs eventArgs)
		{
			var control = sender as TitlePanelControl;
			if (control == null)
				return;
			control.OnTitleChanged((string)eventArgs.OldValue, (string)eventArgs.NewValue);
		}
		
		
		private static void OnPercentageChanged(DependencyObject sender, DependencyPropertyChangedEventArgs eventArgs)
		{
			var control = sender as TitlePanelControl;
			if (control == null)
				return;
			control.OnPercentageChanged((double)eventArgs.OldValue, (double)eventArgs.NewValue);
		}

		public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof (string),
			typeof(TitlePanelControl), new PropertyMetadata(OnTitleChanged));

		public string Title
		{
			get { return (string)GetValue(TitleProperty); } 
			set { SetValue(TitleProperty, value); }
		}


		public static DependencyProperty PercentageProperty = DependencyProperty.Register("Percentage", typeof(double),
			typeof(TitlePanelControl), new PropertyMetadata(OnPercentageChanged));

		public double Percentage
		{
			get { return (double)GetValue(PercentageProperty); } 
			set { SetValue(PercentageProperty, value); }
		}
	}
}