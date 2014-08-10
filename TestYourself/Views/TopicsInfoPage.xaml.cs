using System.Windows;
using Microsoft.Phone.Controls;
using TestYourself.ViewModel;
using TestYourself.ViewModels;

namespace TestYourself.Views
{
	public partial class TopicsInfoPage : PhoneApplicationPage
	{
		public TopicsInfoPage()
		{
			InitializeComponent();
			
		}

		private void TopicsInfoPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			
			//VmLocator.Instance.VmTopicsInfo = new VmTopicsInfo(VmLocator.Instance.VmSubjectPanorama.SelectedTopic, NavigationService);
			DataContext = VmLocator.Instance.VmTopicsInfo;
		}
	}
}