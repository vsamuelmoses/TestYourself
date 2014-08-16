using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TC.CustomControls
{
	public class SliderProgressBar : Slider
	{
		private Dictionary<INeedAcknowledgement, Rectangle> itemToRectangle;

		public SliderProgressBar()
		{
			DefaultStyleKey = typeof(Slider);
			itemToRectangle = new Dictionary<INeedAcknowledgement, Rectangle>();
		}

		public static readonly DependencyProperty NeedAcknowledgementsProperty = DependencyProperty.Register(
			"NeedAcknowledgements", typeof(ObservableCollection<object>), typeof(SliderProgressBar),
			new PropertyMetadata(default(ObservableCollection<object>), OnItemsCollectionChanged));

		private Grid trackGrid;

		private static void OnItemsCollectionChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var thisControl = (SliderProgressBar)dependencyObject;
			thisControl.Refresh();

		}

		private void Refresh()
		{
			itemToRectangle.Clear();
			UnsubscribeToOldCollection();
			if (trackGrid == null)
				return;

			if (NeedAcknowledgements == null)
				return;

			Maximum = NeedAcknowledgements.Count;
			Minimum = 0;
			trackGrid.Children.Clear();
			trackGrid.ColumnDefinitions.Clear();

			var itemCount = 0;
			foreach (var item in NeedAcknowledgements)
			{
				trackGrid.ColumnDefinitions.Add(new ColumnDefinition());

				var rectangle = new Rectangle();
				rectangle.StrokeThickness = 0;

				rectangle.Fill = ((INeedAcknowledgement)item).IsAcknowledged
					? new SolidColorBrush(PhoneAccentBrush)
					: new SolidColorBrush(Colors.Transparent);

				trackGrid.Children.Add(rectangle);

				Grid.SetColumn(rectangle, itemCount);


				itemToRectangle.Add((INeedAcknowledgement)item, rectangle);
				itemCount++;
				((INotifyPropertyChanged)item).PropertyChanged += OnItemPropertyChanged;
			}
		}

		public Color PhoneAccentBrush { get { return (Color)Application.Current.Resources["PhoneAccentColor"]; } }
		void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var item = (INeedAcknowledgement)sender;
			itemToRectangle[item].Fill = item.IsAcknowledged
				? new SolidColorBrush(PhoneAccentBrush)
				: new SolidColorBrush(Colors.Transparent);
		}

		private void UnsubscribeToOldCollection()
		{
			if (NeedAcknowledgements == null)
				return;

			foreach (var item in NeedAcknowledgements)
				((INotifyPropertyChanged)item).PropertyChanged -= OnItemPropertyChanged;
		}

		public ObservableCollection<object> NeedAcknowledgements
		{
			get { return (ObservableCollection<object>)GetValue(NeedAcknowledgementsProperty); }
			set { SetValue(NeedAcknowledgementsProperty, value); }
		}
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			trackGrid = GetTemplateChild("HorizontalTrack") as Grid;
			Refresh();
		}
	}

	public interface INeedAcknowledgement : INotifyPropertyChanged
	{
		bool IsAcknowledged { get; set; }
	}
}
