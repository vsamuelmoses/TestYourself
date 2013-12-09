using System;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TestYourself.Model;
using TestYourself.ViewModel;

namespace TestYourself.View
{
    public partial class TopicsPage1 : PhoneApplicationPage
    {
        public TopicsPage1()
        {
            InitializeComponent();

            //if (VmLocator.Instance.VmTopics.CurrentTopic.Topics.Count > 0)
            //    VmLocator.Instance.VmTopics.CurrentTopic = VmLocator.Instance.VmTopics.CurrentTopic.Topics[0];

            DataContext = VmLocator.Instance.VmTopics;
        }

        private void ListBoxTopicsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            var selectedTopic = listBox.SelectedItem as Topic;

            if (selectedTopic == null)
                return;
            
            if (selectedTopic.SubTopics.Count == 0)
            {
                VmLocator.Instance.VmTopics.SelectedTopic = selectedTopic;
                
                NavigationService.Navigate(new Uri("/View/Question.xaml", UriKind.Relative));
                listBox.SelectedItem = null;
                return;
            }

            VmLocator.Instance.VmTopics = new VmTopics(selectedTopic);
            NavigationService.Navigate(new Uri("/View/TopicsPage2.xaml", UriKind.Relative));
            listBox.SelectedItem = null;
        }


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var viewmodel = DataContext as VmTopics;

            //Bug: when the expander is in expanded state and when the page is navigated away and is navigated back again, then the expanded view is not 
            // properly drawn..
            viewmodel.IsTopicsDetailsVisible = false;
            
            // refresh the datacontext to get the latest updates in the view model - especially the progress in the topics
            DataContext = null;
            DataContext = viewmodel;
        }

        private void ApplicationBarIconButtonClick(object sender, EventArgs e)
        {
            var viewmodelTopics = (VmTopics) DataContext;
            viewmodelTopics.IsTopicsDetailsVisible = !viewmodelTopics.IsTopicsDetailsVisible;

            var button = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
            if (viewmodelTopics.IsTopicsDetailsVisible)
            {
                button.IconUri = new Uri("/Images/CollapseAll.png", UriKind.Relative);
                button.Text = "Collapse all";
            }
            else
            {
                button.IconUri = new Uri("/Images/ExpandAll.png", UriKind.Relative);
                button.Text = "Expand all";
            }
        }
    }
}