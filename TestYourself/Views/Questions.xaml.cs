using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TestYourself.Annotations;
using TestYourself.ResourceDictionaries;
using TestYourself.ViewModels;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace TestYourself.Views
{
	public partial class Questions : PhoneApplicationPage
	{
		private bool isMessageBoxShown = false;
		private VmQuestionContent currentViewingQuestion;
		private readonly ApplicationBarIconButton appBarInfoButton;
		private readonly ApplicationBarIconButton appBarViewHideIcon;

		public Questions()
		{
			InitializeComponent();

			ApplicationBar = new ApplicationBar();
			ApplicationBar.IsVisible = true;
			ApplicationBar.Opacity = 1;
			ApplicationBar.IsMenuEnabled = false;

			appBarInfoButton = new ApplicationBarIconButton();
			appBarInfoButton.IconUri = new Uri("/Images/info.png", UriKind.Relative);
			appBarInfoButton.Text = "info";
			ApplicationBar.Buttons.Add(appBarInfoButton);
			appBarInfoButton.Click += AppBarInfoButtonClick;

			appBarViewHideIcon = new ApplicationBarIconButton();
			appBarViewHideIcon.IconUri = new Uri("/Images/View.png", UriKind.Relative);
			appBarViewHideIcon.Text = "view";
			ApplicationBar.Buttons.Add(appBarViewHideIcon);
			appBarViewHideIcon.Click += AppBarViewHideIconClick;

			VmLocator.Instance.VmTopicQuestions = new VmTopicQuestions(VmLocator.Instance.VmTopicsInfo.Topic, NavigationService);
			DataContext = VmLocator.Instance.VmTopicQuestions;

			MediaViewer.ItemDisplayed += MediaViewer_ItemDisplayed;
		}

		void MediaViewer_ItemDisplayed(object sender, TC.CustomControls.MediaViewer.ItemDisplayedEventArgs e)
		{
			currentViewingQuestion = (VmQuestionContent)((IList)(MediaViewer.Items))[MediaViewer.DisplayedItemIndex];
			SetQuestionNumber(currentViewingQuestion.Question.QuestionNumber);
			SetKeypointText(currentViewingQuestion.Question);
		}

		private void SetKeypointText(Model.Question question)
		{
			if (question.KeyPoint == null || string.IsNullOrEmpty(question.KeyPoint.Text))
			{
				appBarInfoButton.IsEnabled = false;
			}
			else
			{
				appBarInfoButton.IsEnabled = true;
			}
		}

		private void SetQuestionNumber(int questionNumber)
		{
			QuestionNumberTextBlock.Text = questionNumber.ToString();
		}

		private void AppBarViewHideIconClick(object sender, EventArgs e)
		{

			if (ViewModel.IsPopupOpened)
				return;

			currentViewingQuestion.IsResultVisible = !currentViewingQuestion.IsResultVisible;

			SetQuestionNumber(currentViewingQuestion.Question.QuestionNumber);

			appBarViewHideIcon.IconUri = currentViewingQuestion.IsResultVisible 
				? new Uri("/Images/ViewReset.png", UriKind.Relative) 
				: new Uri("/Images/View.png", UriKind.Relative);
		}

		private void AppBarInfoButtonClick(object sender, EventArgs e)
		{
			if (ViewModel.IsPopupOpened)
			{
				ViewModel.ClearPopup();
			}
			else
			{
				ViewModel.SetContentAsPopup(currentViewingQuestion.Question.KeyPoint, MessageBoxResources.Instance.KeypointDataTemplate);
			}
		}

		private VmTopicQuestions ViewModel { get { return (VmTopicQuestions) DataContext; }
		}

		public ObservableCollection<object> QuestionsCollection { get; set; }

		public ObservableCollection<object> Names { get; set; }

		private void MessageBoxPopupGrid_OnTap(object sender, GestureEventArgs e)
		{
			ViewModel.ClearPopup();
		}
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