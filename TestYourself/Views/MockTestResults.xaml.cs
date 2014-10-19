using Microsoft.Phone.Controls;
using TestYourself.ViewModels;

namespace TestYourself.Views
{
	public partial class MockTestResults : PhoneApplicationPage
	{
		public MockTestResults()
		{
			InitializeComponent();
			DataContext = VmLocator.Instance.VmMockTest;
		}
	}
}