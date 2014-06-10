using System;
using System.Collections.ObjectModel;
using System.Windows.Navigation;
using TestYourself.Helpers;
using TestYourself.Model;
using TestYourself.ViewModel;

namespace TestYourself.ViewModels
{
	public class VmTopicQuestions : VmPage
	{
		private readonly NavigationService navigationService;

		public VmTopicQuestions(Topic topic, NavigationService navigationService)
		{
			this.navigationService = navigationService;
			Topic = topic;
			CommandResetProgress = new RelayCommand(notUsed => Topic.ResetProgress());
			CommandGoToQuestions = new RelayCommand(
				notUsed =>
				{
					VmLocator.Instance.VmQuestionNew.Topic = Topic;
					this.navigationService.Navigate(new Uri("/Views.New/Questions.xaml", UriKind.Relative));
				});

			TitleName = Topic.Name;

			Questions = new ObservableCollection<object>();
			foreach(var question in Topic.Questions)
				Questions.Add(new VmQuestionContent() {Topic = Topic, Question = question });
				
		}

		public RelayCommand CommandGoToQuestions { get; set; }
		public RelayCommand CommandResetProgress { get; set; }
		public Topic Topic { get; set; }
		public ObservableCollection<object> Questions { get; set; } 
	}
}
