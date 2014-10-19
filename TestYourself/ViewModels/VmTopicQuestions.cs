using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;
using TestYourself.Helpers;
using TestYourself.Model;

namespace TestYourself.ViewModels
{
	public class VmTopicQuestions : VmPopupHostingPage
	{
		private readonly NavigationService navigationService;
		private ObservableCollection<object> questions;

		public VmTopicQuestions(Topic topic)
		{
			Topic = topic;
			TitleName = Topic.Name;
			Questions = new ObservableCollection<object>();
			foreach (var question in Topic.Questions)
				Questions.Add(new VmQuestionContent(this) { Topic = Topic, Question = question });
			IsPopupOpened = false;
		}


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

		public ObservableCollection<object> Questions
		{
			get { return questions; }
			set
			{
				questions = value;
				InvokePropertyChanged("Questions");
			}
		}
	}
}
