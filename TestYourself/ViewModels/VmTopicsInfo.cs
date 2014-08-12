using System;
using System.Windows.Navigation;
using TestYourself.Helpers;
using TestYourself.Model;

namespace TestYourself.ViewModels 
{
    public class VmTopicsInfo : VmPage
    {
        private readonly NavigationService navigationService;

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

            TitleName = Topic.Name;
        }

        public RelayCommand CommandGoToQuestions { get; set; }
        public RelayCommand CommandResetProgress { get; set; }
        public Topic Topic { get; set; }
    }
}
