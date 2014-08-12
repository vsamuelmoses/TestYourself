using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using TestYourself.Helpers;
using TestYourself.Model;
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
			var subject = new Subject();
			subject.LoadFrom(DatabaseHelper.Databases.First());
			VmLocator.Instance.VmSubjectPanorama = new VmSubjectPanorama(subject, NavigationService);
			DataContext = VmLocator.Instance.VmSubjectPanorama;
		}
	}
}