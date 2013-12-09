using Microsoft.Phone.Controls;
using TestYourself.Helpers;
using TestYourself.ViewModel;
using VmQuestion = TestYourself.ViewModels.VmQuestion;

namespace TestYourself.Views
{
    public partial class Question : PhoneApplicationPage
    {
        public Question()
        {
            InitializeComponent();

            VmLocator.Instance.VmQuestionNew.Topic =VmLocator.Instance.VmSubjectPanorama.SelectedTopic;
            DataContext = VmLocator.Instance.VmQuestionNew;
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            var vmQuestion = (VmQuestion)DataContext;
            var topic = vmQuestion.Topic;

            var topicSettings = AppSettings.Instance.GetTopicSettings(topic);
            topicSettings.LastVisitedQuestionNumber = vmQuestion.Question.QuestionNumber;
            AppSettings.Instance.SetTopicSettings(vmQuestion.Topic, topicSettings);
        }
    }
}