using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using TestYourself.Helpers;
using TestYourself.ViewModel;

namespace TestYourself.View
{
    public partial class QuestionAndAnswerPage : PhoneApplicationPage
    {
        public QuestionAndAnswerPage()
        {
            InitializeComponent();

            VmLocator.Instance.VmQuestion.Topic =VmLocator.Instance.VmTopics.SelectedTopic;
            DataContext = VmLocator.Instance.VmQuestion;
        }

        private void ButtonCheckAnswerClick(object sender, System.Windows.RoutedEventArgs e)
        {
            RevealAnswers(true);

            // by doing this we are making sure we are initilaising/populating the selectedItems, as it is lazy populated.
            // if we dont do this, the binindg for the commandCheckAnswers doesnt populate the listbox answer items as the parameter
            var selectedItems = listBoxAnswerChoices.SelectedItems;
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

        private void ListBoxAnswerChoicesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RevealAnswers(false);

            var vmQuestion = (VmQuestion)DataContext;
            vmQuestion.State = VmQuestion.States.NotAnsweredYet;
        }


        private void RevealAnswers(bool show)
        {
            for (int i = 0; i < listBoxAnswerChoices.Items.Count; i++)
            {
                var listBoxItem = (ListBoxItem)(listBoxAnswerChoices.ItemContainerGenerator.ContainerFromIndex(i));

                if (listBoxItem == null)
                    return;

                var stackPanel =
                    VisualTreeHelper.GetChild(
                        VisualTreeHelper.GetChild(
                            VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(listBoxItem, 0), 0), 0), 0) as
                    StackPanel;

                if (stackPanel == null)
                    return;

                ((Image)stackPanel.Children[2]).Visibility = show ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}