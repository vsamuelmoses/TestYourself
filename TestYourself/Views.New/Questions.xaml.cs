﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TestYourself.Annotations;
using TestYourself.ViewModel;
using TestYourself.ViewModels;

namespace TestYourself.Views.New
{
	public partial class Questions : PhoneApplicationPage
	{
		private bool isMessageBoxShown = false;
		private ApplicationBarIconButton appBarInfoButton;
		private ApplicationBarIconButton appBarViewHideIcon;

		public Questions()
		{
			InitializeComponent();

			ApplicationBar = new ApplicationBar();
			ApplicationBar.IsVisible = true;
			ApplicationBar.Opacity = 1;
			ApplicationBar.IsMenuEnabled = false;

			appBarInfoButton = new ApplicationBarIconButton();
			appBarInfoButton.IconUri = new Uri("/Assets/Images/info.png", UriKind.Relative);
			appBarInfoButton.Text = "info";
			ApplicationBar.Buttons.Add(appBarInfoButton);
			appBarInfoButton.Click += AppBarInfoButtonClick;

			appBarViewHideIcon = new ApplicationBarIconButton();
			appBarViewHideIcon.IconUri = new Uri("/Assets/Images/View.png", UriKind.Relative);
			appBarViewHideIcon.Text = "view";
			ApplicationBar.Buttons.Add(appBarViewHideIcon);
			appBarViewHideIcon.Click += AppBarViewHideIconClick;

			VmLocator.Instance.VmTopicQuestions = new VmTopicQuestions(VmLocator.Instance.VmSubjectPanorama.SelectedTopic, NavigationService);
			DataContext = VmLocator.Instance.VmTopicQuestions;
		}

		private void AppBarViewHideIconClick(object sender, EventArgs e)
		{
			var question = ((IList)(MediaViewer.Items))[MediaViewer.DisplayedItemIndex] as VmQuestionContent;
			question.IsResultVisible = !question.IsResultVisible;

			if (question.IsResultVisible)
				CorrectWrongStampImage.Visibility = System.Windows.Visibility.Visible;
			else
			{
				CorrectWrongStampImage.Visibility = System.Windows.Visibility.Collapsed;
			}
		}
		
		private void AppBarInfoButtonClick(object sender, EventArgs e)
		{
			if (isMessageBoxShown)
			{
				MessageBoxCloseTransform.Begin();
				isMessageBoxShown = false;
				LayoutRoot.Opacity = 1.0;
			}
			else
			{
				LayoutRoot.Opacity = 0.3;
				MessageBoxOpenTransform.Begin();
				isMessageBoxShown = true;
			}
		}

		public ObservableCollection<object> QuestionsCollection { get; set; }

		public ObservableCollection<object> Names { get; set; }
	}

	public class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}