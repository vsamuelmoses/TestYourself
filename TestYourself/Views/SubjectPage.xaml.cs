using System;
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
			FeedbackOverlay.VisibilityChanged += FeedbackOverlay_VisibilityChanged;
		}

		void FeedbackOverlay_VisibilityChanged(object sender, EventArgs e)
		{
			if(ApplicationBar != null)
				ApplicationBar.IsVisible = (FeedbackOverlay.Visibility != Visibility.Visible);
		}

		void OnSubjectPageLoaded(object sender, RoutedEventArgs e)
		{
			
			VmLocator.Instance.VmSubjectPanorama = new VmSubjectPanorama(subject, NavigationService);
			DataContext = VmLocator.Instance.VmSubjectPanorama;
		}

		private void Reset_Click(object sender, EventArgs e)
		{
			FeedbackOverlay.Reset();
		}
	}
}