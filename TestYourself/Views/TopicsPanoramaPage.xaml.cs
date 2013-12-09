using System.Windows;
using Microsoft.Phone.Controls;
using TestYourself.ViewModel;
using TestYourself.ViewModels;

namespace TestYourself.Views
{
    public partial class TopicsPanoramaPage : PhoneApplicationPage
    {
        public TopicsPanoramaPage()
        {
            InitializeComponent();
        }

        private void TopicsPanoramaPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            VmLocator.Instance.VmSubjectPanorama = new VmSubjectPanorama(VmLocator.Instance.VmSubjects.DefaultSubject, NavigationService);
            DataContext = VmLocator.Instance.VmSubjectPanorama;
        }
    }
}