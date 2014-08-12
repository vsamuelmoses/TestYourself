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
		private readonly Subject subject;

		public SubjectPage()
		{
			InitializeComponent();
			Loaded += OnSubjectPageLoaded;

			subject = new Subject();
			subject.LoadFrom(DatabaseHelper.Databases.First());
		}

		void OnSubjectPageLoaded(object sender, RoutedEventArgs e)
		{
			
			VmLocator.Instance.VmSubjectPanorama = new VmSubjectPanorama(subject, NavigationService);
			DataContext = VmLocator.Instance.VmSubjectPanorama;
		}
	}
}