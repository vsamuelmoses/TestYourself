using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TC.CustomControls
{
	public class SliderProgressBar : Slider
	{
		private Dictionary<INeedAcknowledgement, Rectangle> itemToRectangle;
		private Dictionary<Rectangle, List<INeedAcknowledgement>> rectangleToItems;
		private Dictionary<int, int> slotWeightage;

		public SliderProgressBar()
		{
			DefaultStyleKey = typeof(Slider);
			itemToRectangle = new Dictionary<INeedAcknowledgement, Rectangle>();
			rectangleToItems = new Dictionary<Rectangle, List<INeedAcknowledgement>>();
			LayoutUpdated += OnSliderProgressBarLayoutUpdated;
		}

		private void SetTotalSlotsAndWeightage(int totalSlots, int totalItems)
		{
			slotWeightage = new Dictionary<int, int>();
			var multiplesOfItemsInASlot = totalItems / totalSlots;
			var reminderItems = totalItems - (multiplesOfItemsInASlot * totalSlots);

			slotWeightage = new Dictionary<int, int>();

			for (int i = 0; i < totalSlots; i++)
				slotWeightage.Add(i, multiplesOfItemsInASlot);

			for (int i = (totalSlots - reminderItems); i < totalSlots; i++)
				slotWeightage[i] = slotWeightage[i] + 1;
		}

		private void OnSliderProgressBarLayoutUpdated(object sender, EventArgs e)
		{
			if (NeedAcknowledgements == null || NeedAcknowledgements.Count == 0)
				return;

			if (!isRefreshedAfterLayoutUpdated)
			{
				Refresh();
				//UpdateHolderWidth();
				isRefreshedAfterLayoutUpdated = true;
			}
		}

		public static readonly DependencyProperty NeedAcknowledgementsProperty = DependencyProperty.Register(
			"NeedAcknowledgements", typeof(ObservableCollection<object>), typeof(SliderProgressBar),
			new PropertyMetadata(default(ObservableCollection<object>), OnItemsCollectionChanged));

		private Grid trackGrid;
		private Rectangle trackHolder;
		private bool isRefreshedAfterLayoutUpdated;
		private bool trackHolderWidthIsSet;

		private static void OnItemsCollectionChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			var thisControl = (SliderProgressBar)dependencyObject;
			thisControl.Refresh();

		}

		private void Refresh()
		{
			itemToRectangle.Clear();
			UnsubscribeToOldCollection();
			if (trackGrid == null || double.IsNaN(trackGrid.ActualWidth))
				return;

			if (NeedAcknowledgements == null)
				return;

			var totalSlots = (int)trackGrid.ActualWidth;
			var totalItems = NeedAcknowledgements.Count;

			if (totalItems < totalSlots)
				totalSlots = totalItems;

			SetTotalSlotsAndWeightage(totalSlots, totalItems);

			Maximum = NeedAcknowledgements.Count - 1;
			SmallChange = 1;
			Minimum = 0;
			trackGrid.Children.Clear();
			trackGrid.ColumnDefinitions.Clear();

			int itemCount = 0;

			for (int i = 0; i < totalSlots; i++)
			{
				trackGrid.ColumnDefinitions.Add(new ColumnDefinition());

				var rectangle = new Rectangle();
				rectangle.StrokeThickness = 0;

				var items = new List<INeedAcknowledgement>();
				for (int noOfItems = 0; noOfItems < slotWeightage[i]; noOfItems++)
				{
					var item = NeedAcknowledgements[itemCount++];
					items.Add((INeedAcknowledgement)item);
					itemToRectangle.Add((INeedAcknowledgement)item, rectangle);
					((INeedAcknowledgement)item).PropertyChanged += OnItemPropertyChanged;
				}
				rectangleToItems.Add(rectangle, items);

				rectangle.Fill = items.Any(item => !item.IsAcknowledged)
					? new SolidColorBrush(Colors.Transparent)
					: new SolidColorBrush(PhoneAccentBrush);

				trackGrid.Children.Add(rectangle);
				Grid.SetColumn(rectangle, i);
			}

			//UpdateHolderWidth();
		}

		private void UpdateHolderWidth()
		{
			if (!itemToRectangle.Any() || trackHolderWidthIsSet ||
				double.IsNaN(itemToRectangle.First().Value.ActualWidth) ||
				itemToRectangle.First().Value.ActualWidth == 0)
				return;

			var width = itemToRectangle.First().Value.ActualWidth;

			if (width < 4)
				width = 4;

			trackHolder.Width = width;

			trackHolderWidthIsSet = true;
		}

		public Color PhoneAccentBrush { get { return (Color)Application.Current.Resources["PhoneAccentColor"]; } }
		void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var item = (INeedAcknowledgement)sender;
			var rectangle = itemToRectangle[item];
			var itemsInThisRectangle = rectangleToItems[rectangle];

			itemToRectangle[item].Fill = itemsInThisRectangle.Any(i => !i.IsAcknowledged)
				? new SolidColorBrush(Colors.Transparent)
				: new SolidColorBrush(PhoneAccentBrush);
		}

		private void UnsubscribeToOldCollection()
		{
			if (NeedAcknowledgements == null)
				return;

			foreach (var item in NeedAcknowledgements)
				((INeedAcknowledgement)item).PropertyChanged -= OnItemPropertyChanged;
		}

		public ObservableCollection<object> NeedAcknowledgements
		{
			get { return (ObservableCollection<object>)GetValue(NeedAcknowledgementsProperty); }
			set { SetValue(NeedAcknowledgementsProperty, value); }
		}
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			trackHolder = GetTemplateChild("HorizontalCenterElement") as Rectangle;

			if(trackGrid != null)
				trackGrid.LayoutUpdated -= OnTrackGridLayoutUpdated;

			trackGrid = GetTemplateChild("HorizontalTrack") as Grid;

			if (trackGrid != null)
				trackGrid.LayoutUpdated += OnTrackGridLayoutUpdated;
		}

		void OnTrackGridLayoutUpdated(object sender, EventArgs e)
		{
			UpdateHolderWidth();
		}
	}

	public interface INeedAcknowledgement : INotifyPropertyChanged
	{
		bool IsAcknowledged { get; set; }
	}
}
