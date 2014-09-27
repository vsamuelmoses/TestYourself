using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TestYourself.Views
{
	public partial class ImageControl : UserControl
	{
		private double initialAngle;
		private double initialScale;

		public ImageControl()
		{
			InitializeComponent();
		}

		private void OnPinchStarted(object sender, PinchStartedGestureEventArgs e)
		{
			initialAngle = transform.Rotation;
			initialScale = transform.ScaleX;
		}

		private void OnPinchDelta(object sender, PinchGestureEventArgs e)
		{
			transform.Rotation = initialAngle + e.TotalAngleDelta;
			transform.ScaleX = initialScale * e.DistanceRatio;
			transform.ScaleY = initialScale * e.DistanceRatio;
		}
	}
}
