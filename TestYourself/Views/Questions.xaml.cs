using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TestYourself.Annotations;
using TestYourself.Helpers;
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
		private bool isLoading;

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

			MediaViewer.ItemDisplayed += MediaViewer_ItemDisplayed;
			IsLoading = true;

			Task.Factory.StartNew(() =>
			{
				return new VmTopicQuestions(VmLocator.Instance.VmTopicsInfo.Topic);
			}).ContinueWith(task =>
			{
				IsLoading = false;
				VmLocator.Instance.VmTopicQuestions = task.Result;
				DataContext = VmLocator.Instance.VmTopicQuestions;
				Loaded += OnLoaded;
				Unloaded += OnUnloaded;
				questionNavigationPanel.MediaViewer = MediaViewer;

			}, TaskScheduler.FromCurrentSynchronizationContext());
		}

		public bool IsLoading
		{
			get { return isLoading; }
			set
			{
				isLoading = value;

				if (isLoading)
				{
					loadingTextBlock.Text = string.Format("Loading questions for " + VmLocator.Instance.VmTopicsInfo.Topic.Name);
					loadingViewer.Visibility = Visibility.Visible;
					ApplicationBar.IsVisible = false;
					LayoutRoot.Visibility = Visibility.Collapsed;

				}
				else
				{
					loadingViewer.Visibility = Visibility.Collapsed;
					LayoutRoot.Visibility = Visibility.Visible;
					ApplicationBar.IsVisible = true;
					
				}
			}
		}

		protected override void OnBackKeyPress(CancelEventArgs e)
		{
			if (ViewModel.IsPopupOpened)
			{
				ViewModel.ClearPopup();
				e.Cancel = true;
			}
			else
			{
				base.OnBackKeyPress(e);
			}
		}

		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			var topicSettings = AppSettings.Instance.GetTopicSettings(ViewModel.Topic);
			topicSettings.LastVisitedQuestionNumber = currentViewingQuestion.Question.Index;
			AppSettings.Instance.SetTopicSettings(ViewModel.Topic, topicSettings);
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			var topicSettings = AppSettings.Instance.GetTopicSettings(ViewModel.Topic);
			if (topicSettings.LastVisitedQuestionNumber.HasValue)
				MediaViewer.JumpToItem(topicSettings.LastVisitedQuestionNumber.Value - 1); // index is 0 based
		}

		void MediaViewer_ItemDisplayed(object sender, TC.CustomControls.MediaViewer.ItemDisplayedEventArgs e)
		{
			currentViewingQuestion = (VmQuestionContent)((IList)(MediaViewer.Items))[MediaViewer.DisplayedItemIndex];
			SetQuestionNumber(currentViewingQuestion.Question.Index);
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

		private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			MediaViewer.JumpToItem((int)e.NewValue);
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