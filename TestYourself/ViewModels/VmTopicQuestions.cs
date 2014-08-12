using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;
using TestYourself.Helpers;
using TestYourself.Model;

namespace TestYourself.ViewModels
{
	public class VmTopicQuestions : VmPage
	{
		private readonly NavigationService navigationService;
		private bool isPopupOpened;
		private object popupContent;
		private DataTemplate popupContentTemplate;

		public VmTopicQuestions(Topic topic, NavigationService navigationService)
		{
			this.navigationService = navigationService;
			Topic = topic;
			CommandResetProgress = new RelayCommand(notUsed => Topic.ResetProgress());
			CommandGoToQuestions = new RelayCommand(
				notUsed =>
				{
					//VmLocator.Instance.VmQuestionNew.Topic = Topic;
					this.navigationService.Navigate(new Uri("/Views/Questions.xaml", UriKind.Relative));
				});

			TitleName = Topic.Name;

			Questions = new ObservableCollection<object>();
			foreach(var question in Topic.Questions)
				Questions.Add(new VmQuestionContent(this) {Topic = Topic, Question = question });

			IsPopupOpened = false;

		}

		public RelayCommand CommandGoToQuestions { get; set; }
		public RelayCommand CommandResetProgress { get; set; }
		public Topic Topic { get; set; }
		public ObservableCollection<object> Questions { get; set; }

		public bool IsPopupOpened
		{
			get { return isPopupOpened; }
			set
			{
				isPopupOpened = value;
				InvokePropertyChanged("IsPopupOpened");
			}
		}


		public object PopupContent
		{
			get { return popupContent; }
			set
			{
				popupContent = value;
				InvokePropertyChanged("PopupContent");
			}
		}

		public DataTemplate PopupContentTemplate
		{
			get { return popupContentTemplate; }
			set
			{
				popupContentTemplate = value;
				InvokePropertyChanged("PopupContentTemplate");
			}
		}

		public void SetContentAsPopup(object content, DataTemplate contentTemplate)
		{
			PopupContent = content;
			PopupContentTemplate = contentTemplate;
			IsPopupOpened = true;
		}

		public void ClearPopup()
		{
			IsPopupOpened = true;
			IsPopupOpened = false;
		}
	}
}
