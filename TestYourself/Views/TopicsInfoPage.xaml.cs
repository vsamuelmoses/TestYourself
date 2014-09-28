using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.Phone.Controls;
using TestYourself.ViewModels;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace TestYourself.Views
{
	public partial class TopicsInfoPage : PhoneApplicationPage
	{
		public TopicsInfoPage()
		{
			InitializeComponent();

		}

		protected override void OnBackKeyPress(CancelEventArgs e)
		{
			if (ViewModel.IsPopupOpened)
			{
				ViewModel.IsPopupOpened = false;
				e.Cancel = true;
			}
			else
			{
				base.OnBackKeyPress(e);
			}
		}

		private void TopicsInfoPage_OnLoaded(object sender, RoutedEventArgs e)
		{

			//VmLocator.Instance.VmTopicsInfo = new VmTopicsInfo(VmLocator.Instance.VmSubjectPanorama.SelectedTopic, NavigationService);
			DataContext = VmLocator.Instance.VmTopicsInfo;
		}

		private void OnViewHelpButtonClicked(object sender, EventArgs e)
		{
			ViewModel.IsPopupOpened = !(ViewModel.IsPopupOpened);
		}

		private VmTopicsInfo ViewModel
		{
			get { return (VmTopicsInfo) DataContext; }
		}

		private void MessageBoxPopupGrid_OnTap(object sender, GestureEventArgs e)
		{
			ViewModel.IsPopupOpened = false;
		}

		private void OnHelpButtonClick(object sender, RoutedEventArgs e)
		{
			ViewModel.IsPopupOpened = !(ViewModel.IsPopupOpened);
		}
	}
}