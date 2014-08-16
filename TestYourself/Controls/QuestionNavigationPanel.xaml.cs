using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TC.CustomControls;

namespace TestYourself.Controls
{
	public partial class QuestionNavigationPanel : UserControl
	{
		public static readonly DependencyProperty NeedAcknowledgementsProperty = DependencyProperty.Register(
			"NeedAcknowledgements", typeof(ObservableCollection<object>), typeof(QuestionNavigationPanel),
			new PropertyMetadata(default(ObservableCollection<object>), OnItemsCollectionChanged));

		private static void OnItemsCollectionChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var thisControl = (QuestionNavigationPanel)dependencyObject;
			thisControl.SliderProgressBar.NeedAcknowledgements = thisControl.NeedAcknowledgements;

		}
		public ObservableCollection<object> NeedAcknowledgements
		{
			get { return (ObservableCollection<object>)GetValue(NeedAcknowledgementsProperty); }
			set { SetValue(NeedAcknowledgementsProperty, value); }
		}

		public QuestionNavigationPanel()
		{
			InitializeComponent();
		}

		private void OnPreviousButtonClicked(object sender, RoutedEventArgs e)
		{
			if (sliderProgressBar.Value > 0)
				sliderProgressBar.Value--;
		}

		private void OnNextButtonClicked(object sender, RoutedEventArgs e)
		{
			if (sliderProgressBar.Value < sliderProgressBar.Maximum)
				sliderProgressBar.Value++;
		}

		public SliderProgressBar SliderProgressBar { get { return sliderProgressBar; } }

	}
}
