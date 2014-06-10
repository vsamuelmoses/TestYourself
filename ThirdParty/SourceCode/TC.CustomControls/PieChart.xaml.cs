using System.Windows;
using System.Windows.Controls;
using TC.CustomControls.ViewModels;

namespace TC.CustomControls
{
	public partial class PieChart : UserControl
	{
		public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register(
			"Progress", typeof (double), typeof (PieChart), 
			new PropertyMetadata(default(double), PropertyChangedCallback));

		private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
		{
			var circularProgressBar = (PieChart)dependencyObject;

			circularProgressBar.Data = new PieDataCollection
			{
				new PieData {Title = string.Empty},
				new PieData {Title = string.Empty}
			};

			var progress = (double) eventArgs.NewValue;
			if ((double) eventArgs.NewValue > 100)
				progress = 100;
			if ((double)eventArgs.NewValue < 0)
				progress = 0;

			SetValue(circularProgressBar, progress);
			circularProgressBar.Data[0].Title = circularProgressBar.ProgressTitle;
			circularProgressBar.Data[1].Value = 100 - progress;

			RefreshCircularProgressBar(circularProgressBar);
		}

		private static void SetValue(PieChart circularProgressBar, double progress)
		{
			circularProgressBar.Data[0].Value = progress;
			circularProgressBar.ProgressValueTextBlock.Text = progress + " %";
		}

		private static void RefreshCircularProgressBar(PieChart circularProgressBar)
		{
			circularProgressBar.AmPieChart.DataSource = null;
			circularProgressBar.AmPieChart.DataSource = circularProgressBar.Data;
		}

		public double Progress
		{
			get { return (double) GetValue(ProgressProperty); }
			set { SetValue(ProgressProperty, value); }
		}

		public static readonly DependencyProperty ProgressTitleProperty = DependencyProperty.Register(
			"ProgressTitle", typeof (string), typeof (PieChart), 
			new PropertyMetadata(string.Empty, OnProgressTitleChanged));

		private static void OnProgressTitleChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
		{
			var circularProgressBar = (PieChart) dependencyObject;

			if (circularProgressBar.Data != null && circularProgressBar.Data.Count >= 1)
			{
				circularProgressBar.Data[0].Title = (string)eventArgs.NewValue;
				circularProgressBar.LegendTextBlock.Text = (string)eventArgs.NewValue;
				RefreshCircularProgressBar(circularProgressBar);
			}
		}

		public string ProgressTitle
		{
			get { return (string) GetValue(ProgressTitleProperty); }
			set { SetValue(ProgressTitleProperty, value); }
		}

		public PieChart()
		{
			InitializeComponent();
			Data = new PieDataCollection {new PieData(), new PieData()};
		}

		public PieDataCollection Data { get; set; }
	}
}
