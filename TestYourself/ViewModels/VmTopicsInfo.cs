using System;
using System.Windows.Navigation;
using TestYourself.Helpers;
using TestYourself.Model;

namespace TestYourself.ViewModels 
{
    public class VmTopicsInfo : VmPage
    {
        private readonly NavigationService navigationService;
	    private bool isPopupOpened;

	    public VmTopicsInfo(Topic topic, NavigationService navigationService)
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
			CommandMockTest = new RelayCommand(
                notUsed =>
                {
                    VmLocator.Instance.VmMockTest = new VmTimedTopicMockTest(Topic, this.navigationService);
                    this.navigationService.Navigate(new Uri("/Views/MockTest.xaml", UriKind.Relative));
                });

            TitleName = Topic.Name;
        }

		public bool IsPopupOpened
		{
			get { return isPopupOpened; }
			set
			{
				isPopupOpened = value;
				InvokePropertyChanged("IsPopupOpened");
			}
		}

        public RelayCommand CommandGoToQuestions { get; set; }
		public RelayCommand CommandMockTest { get; set; }
        public RelayCommand CommandResetProgress { get; set; }
        public Topic Topic { get; set; }
    }
}
