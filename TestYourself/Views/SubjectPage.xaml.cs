using System.Windows;
using Microsoft.Phone.Controls;
using TestYourself.ViewModel;
using TestYourself.ViewModels;

namespace TestYourself.Views
{
	public partial class SubjectPage : PhoneApplicationPage
	{
		public SubjectPage()
		{
			InitializeComponent();
			Loaded += OnSubjectPageLoaded;
		}

		void OnSubjectPageLoaded(object sender, RoutedEventArgs e)
		{
			VmLocator.Instance.VmSubjectPanorama = new VmSubjectPanorama(VmLocator.Instance.VmSubjects.DefaultSubject, NavigationService);
			DataContext = VmLocator.Instance.VmSubjectPanorama;
		}
	}
}