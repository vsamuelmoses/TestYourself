using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Navigation;
using TestYourself.Helpers;
using TestYourself.Model;
using TestYourself.ViewModel;

namespace TestYourself.ViewModels
{
	public abstract class VmTopicsHostBase : VmPage
	{
		private Topic selectedTopic;
		private ObservableCollection<Topic> topics;
		private bool isTopicsDetailsVisible;
		private int totalNumberOfQuestions;
		private double progress;
		private readonly NavigationService navigationService;

		protected VmTopicsHostBase(NavigationService navigationService)
		{
			this.navigationService = navigationService;
		}

		public Topic SelectedTopic
		{
			get { return selectedTopic; }
			set
			{
				if (selectedTopic == value)
					return;
				selectedTopic = value;

				if (selectedTopic == null)
					return;

				if (selectedTopic.SubTopics.Any())
				{
					var topicsPanoramaUri = 
						new Uri(string.Format("{0}?Id={1}", "/Views/TopicsPanoramaPage.xaml", Guid.NewGuid()), UriKind.Relative);
					VmLocator.Instance.VmTopicsPanorama = new VmTopicsPanorama(selectedTopic, navigationService);
					navigationService.Navigate(topicsPanoramaUri);
				}
				else
				{
					VmLocator.Instance.VmTopicsInfo = new VmTopicsInfo(selectedTopic, navigationService);
					navigationService.Navigate(new Uri("/Views/TopicsInfoPage.xaml", UriKind.Relative));
				}
			}
		}

		public ObservableCollection<Topic> Topics
		{
			get { return topics; }
			set
			{
				topics = value;
				InvokePropertyChanged("Topics");
				TotalNumberOfQuestions = topics.Sum(topic => topic.TotalNumberOfQuestions);
			}
		}

		public bool IsTopicsDetailsVisible
		{
			get { return isTopicsDetailsVisible; }
			set
			{
				isTopicsDetailsVisible = value;
				InvokePropertyChanged("IsTopicsDetailsVisible");
			}
		}

		public int TotalNumberOfQuestions
		{
			get { return totalNumberOfQuestions; }
			set
			{
				totalNumberOfQuestions = value;
				InvokePropertyChanged("TotalNumberOfQuestions");
			}
		}

		public abstract double SuccessPercentage { get; }
		
		public double Progress
		{
			get { return progress; }
			set
			{
				progress = value;
				InvokePropertyChanged("Progress");
			}
		}

		public abstract RelayCommand CommandResetProgress { get; set; }
	}

	public class VmSubjectPanorama : VmTopicsHostBase
	{
		private readonly NavigationService navigationService;

		public VmSubjectPanorama(Subject subject, NavigationService navigationService) 
			: base(navigationService)
		{
			ParentSubject = subject;
			CommandResetProgress = new RelayCommand(notUsed => ParentSubject.ResetProgress());
		}

		private Subject parentSubject;



		public Subject ParentSubject
		{
			get { return parentSubject; }
			set
			{
				parentSubject = value;
				InvokePropertyChanged("ParentSubject");

				Topics = new ObservableCollection<Topic>(parentSubject.Topics);
				TitleName = parentSubject.Name;
				//SuccessPercentage = parentSubject.SuccessPercentage;
				Progress = parentSubject.PercentageWorked;
			}
		}

		public override double SuccessPercentage
		{
			get { return ParentSubject.SuccessPercentage; }
		}

		public override sealed RelayCommand CommandResetProgress { get; set; }
	}
}
