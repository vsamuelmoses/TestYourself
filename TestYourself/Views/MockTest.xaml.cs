using Microsoft.Phone.Controls;
using TestYourself.ViewModels;

namespace TestYourself.Views
{
	public partial class MockTest : PhoneApplicationPage
	{
		public MockTest()
		{
			InitializeComponent();
			DataContext = VmLocator.Instance.VmMockTest;
		}
	}
}