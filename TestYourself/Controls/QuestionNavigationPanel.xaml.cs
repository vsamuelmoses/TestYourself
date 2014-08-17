using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using TC.CustomControls;
using TC.CustomControls.MediaViewer;

namespace TestYourself.Controls
{
	public partial class QuestionNavigationPanel : UserControl
	{
		public static readonly DependencyProperty NeedAcknowledgementsProperty = DependencyProperty.Register(
			"NeedAcknowledgements", typeof(ObservableCollection<object>), typeof(QuestionNavigationPanel),
			new PropertyMetadata(default(ObservableCollection<object>), OnItemsCollectionChanged));

		private MediaViewer mediaViewer;
		private Dispatcher dispatcher;

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

			dispatcher = Deployment.Current.Dispatcher;

			var sliderValueChangingEvents = Observable.FromEventPattern<RoutedPropertyChangedEventHandler<double>, 
				RoutedPropertyChangedEventArgs<double>>(
				handler => sliderProgressBar.ValueChanged += handler,
				handler => sliderProgressBar.ValueChanged -= handler);

			var observable = sliderValueChangingEvents
				.Throttle(TimeSpan.FromMilliseconds(500))
				.Subscribe(args => JumpToQuestionWithSliderProgressValue());


			//sliderProgressBar.ValueChanged += OnSliderProgressBarValueChanged;
		}

		//void OnSliderProgressBarValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		private void JumpToQuestionWithSliderProgressValue()
		{
			dispatcher.BeginInvoke(() =>
			{
				if (MediaViewer == null)
					return;

				if (MediaViewer.DisplayedItemIndex == sliderProgressBar.Value)
					return;


					MediaViewer.JumpToItem((int) sliderProgressBar.Value);
			});
		}

		private void OnPreviousButtonClicked(object sender, RoutedEventArgs e)
		{
			if (sliderProgressBar.Value > 0)
			{
				for (int i = (int)sliderProgressBar.Value - 1; i >= 0; i--)
				{
					if (!((INeedAcknowledgement)NeedAcknowledgements[i]).IsAcknowledged)
					{
						sliderProgressBar.Value = i;
						return;
					}
				}

			}
		}

		private void OnNextButtonClicked(object sender, RoutedEventArgs e)
		{
			if (sliderProgressBar.Value < sliderProgressBar.Maximum)
			{
				for (int i = (int) sliderProgressBar.Value + 1; i <= sliderProgressBar.Maximum; i++)
				{
					if (!((INeedAcknowledgement) NeedAcknowledgements[i]).IsAcknowledged)
					{
						sliderProgressBar.Value = i;
						return;
					}
				}

			}
		}

		public SliderProgressBar SliderProgressBar { get { return sliderProgressBar; } }

		public MediaViewer MediaViewer
		{
			get { return mediaViewer; }
			set
			{
				if(mediaViewer != null)
					mediaViewer.ItemDisplayed -= OnMediaViewerItemDisplayed;
				
				mediaViewer = value;
				mediaViewer.ItemDisplayed += OnMediaViewerItemDisplayed;
			}
		}

		void OnMediaViewerItemDisplayed(object sender, ItemDisplayedEventArgs e)
		{
			if (sliderProgressBar.Value == MediaViewer.DisplayedItemIndex)
				return;

			sliderProgressBar.Value = MediaViewer.DisplayedItemIndex;
		}
	}
}
