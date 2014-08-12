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
		}
	}
}