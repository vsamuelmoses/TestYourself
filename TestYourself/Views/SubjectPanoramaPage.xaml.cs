using System.Windows;
using Microsoft.Phone.Controls;
using TestYourself.ViewModel;
using TestYourself.ViewModels;

namespace TestYourself.Views
{
    public partial class SubjectPanoramaPage : PhoneApplicationPage
    {
        public SubjectPanoramaPage()
        {
            InitializeComponent();
            DataContext = VmLocator.Instance.VmSubjects;
        }

        private void SubjectPanoramaPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            VmLocator.Instance.VmSubjectPanorama = new VmSubjectPanorama(VmLocator.Instance.VmSubjects.DefaultSubject, NavigationService);
            DataContext = VmLocator.Instance.VmSubjectPanorama;
        }
    }
}