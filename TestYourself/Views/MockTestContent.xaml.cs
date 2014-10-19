using Microsoft.Phone.Controls;
using TestYourself.ViewModels;

namespace TestYourself.Views
{
	public partial class MockTestContent : PhoneApplicationPage
	{
		public MockTestContent()
		{
			InitializeComponent();
			DataContext = VmLocator.Instance.VmMockTest;

			Loaded += MockTestContent_Loaded;
		}

		void MockTestContent_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			VmMockTest.Start();
		}

		public VmMockTest VmMockTest {get { return (VmMockTest)DataContext; }}
	}
}