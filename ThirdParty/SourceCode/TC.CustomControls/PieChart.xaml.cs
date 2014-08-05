using System.Windows;
using System.Windows.Controls;
using TC.CustomControls.ViewModels;

namespace TC.CustomControls
{
	public partial class PieChart : ContentControl
	{
		public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register(
			"Progress", typeof (double), typeof (PieChart), 
			new PropertyMetadata(default(double), PropertyChangedCallback));

		private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
		{
			var circularProgressBar = (PieChart)dependencyObject;
			RefreshCircularProgressBar(circularProgressBar);
		}

		private void PrepareData()
		{
			Data = new PieDataCollection
			{
				new PieData {Title = string.Empty},
				new PieData {Title = string.Empty}
			};

			var progress = Progress;
			if ((double) Progress > 100)
				progress = 100;
			if ((double) Progress < 0)
				progress = 0;

			Data[0].Value = progress;
			Data[0].Title = ProgressTitle;
			Data[1].Value = 100 - progress;

			ProgressValueTextBlock.Text = progress + " %";
		}

		private static void RefreshCircularProgressBar(PieChart circularProgressBar)
		{
			circularProgressBar.PrepareData();
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
				//RefreshCircularProgressBar(circularProgressBar);
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
			Loaded += PieChart_Loaded;
		}

		void PieChart_Loaded(object sender, RoutedEventArgs e)
		{
			RefreshCircularProgressBar(this);
		}

		public PieDataCollection Data { get; set; }
	}
}
