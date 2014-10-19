using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Navigation;
using TestYourself.Helpers;

namespace TestYourself.ViewModels
{
	public abstract class VmMockTest : VmPopupHostingPage
	{
		private readonly NavigationService navigationService;
		private int numberOfQuestions = 5;


		public enum TestStatus
		{
			NotStarted,
			InProgress,
			Aborted,
			Completed,
		}

		protected VmMockTest(NavigationService navigationService)
		{
			this.navigationService = navigationService;
			Questions = new ObservableCollection<object>();
			CommandStartTest = new RelayCommand(
				notUsed =>
				{
					//VmLocator.Instance.VmQuestionNew.Topic = Topic;
					this.navigationService.Navigate(new Uri("/Views/MockTestContent.xaml", UriKind.Relative));
				});

			CommandEndTest = new RelayCommand(
				notUsed =>
				{
					//VmLocator.Instance.VmQuestionNew.Topic = Topic;
					this.navigationService.Navigate(new Uri("/Views/MockTestResults.xaml", UriKind.Relative));

					foreach (var question in Questions)
					{
						((VmTestQuestionContent) question).IsResultVisible = true;
					}
				});
		}

		public TestStatus State { get; set; }
		public double? Percentage { get; set; }

		public int NumberOfQuestions
		{
			get { return numberOfQuestions; }
			set
			{
				if (numberOfQuestions == value)
					return;

				numberOfQuestions = value;
				InvokePropertyChanged("NumberOfQuestions");
			}
		}

		public ObservableCollection<object> Questions { get; set; }

		public abstract void Start();
		public abstract void Pause();
		public abstract void Finish();

		public abstract void Resume();

		public RelayCommand CommandStartTest { get; set; }
		public RelayCommand CommandEndTest { get; set; }
	}
}