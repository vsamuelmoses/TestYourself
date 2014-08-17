using Microsoft.Phone.Controls;
using TestYourself.ViewModels;

namespace TestYourself.Views
{
	public partial class TopicsPanoramaPage : PhoneApplicationPage
	{
		public TopicsPanoramaPage()
		{
			InitializeComponent();
			DataContext = VmLocator.Instance.VmTopicsPanorama;
			this.Loaded += TopicsPanoramaPage_Loaded;
		}

		void TopicsPanoramaPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			topicsListBox.SelectedItem = null;
		}
	}
}