using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TestYourself.Model;
using TestYourself.ViewModel;

namespace TestYourself.View
{
    public partial class SubjectsPage : PhoneApplicationPage
    {
        private BackgroundWorker bw;
        private DispatcherTimer timer;
        private double progressPercentage;

        public SubjectsPage()
        {
            InitializeComponent();
            //DataContext = VmLocator.Instance.VmSubjects;

            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += BwDoWork;
            bw.ProgressChanged += BwProgressChanged;
            bw.RunWorkerCompleted += BwRunWorkerCompleted;

            SetLoadingInProgress(true);

            bw.RunWorkerAsync();
        }

        private void BwDoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            var vmSubjects = VmLocator.Instance.VmSubjects;
            worker.ReportProgress(100);
        }

        private void BwRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetLoadingInProgress(false);
            DataContext = VmLocator.Instance.VmSubjects;
        }

        private void BwProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        private void SetLoadingInProgress(bool isTrue)
        {
            progressBarLoadingSubjects.IsIndeterminate = isTrue;
            stackPanelProgressBar.Visibility = isTrue ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            ApplicationBar.IsVisible = !isTrue;
            
            if (isTrue)
                EnableProgressSimulationTimer();
            else
                DisableProgressSimulationTimer();
        }

        private void DisableProgressSimulationTimer()
        {
            timer.Stop();
            timer.Tick -= TimerTick;
        }

        private void EnableProgressSimulationTimer()
        {
            progressPercentage = 0;
            if (timer == null)
                timer = new DispatcherTimer();

            timer.Tick += TimerTick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 350);
            timer.Start();
        }

        void TimerTick(object sender, EventArgs e)
        {
            if(progressPercentage < 100)
                textBlockLoadingPercentage.Text = "Loading... " + (progressPercentage++) + "%";   
        }

        private void ListBoxSubjectsSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedSubject = listBoxSubjects.SelectedItem as Subject;

            if (selectedSubject == null)
                return;

            VmLocator.Instance.VmSubjects.SelectedSubject = selectedSubject;
            VmLocator.Instance.VmTopics = new VmTopics(selectedSubject);
            NavigationService.Navigate(new Uri("/View/TopicsPage1.xaml", UriKind.Relative));
            listBoxSubjects.SelectedItem = null;
        }


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var viewmodel = DataContext as VmSubjects;

            if (viewmodel == null)
                return;

            //Bug: when the expander is in expanded state and when the page is navigated away and is navigated back again, then the expanded view is not 
            // properly drawn..
            viewmodel.IsDetailsVisible = false;

            // refresh the datacontext to get the latest updates in the view model - especially the progress in the topics
            DataContext = null;
            DataContext = viewmodel;
        }

        private void ApplicationBarIconButtonClick(object sender, EventArgs e)
        {
            var viewmodelTopics = (VmSubjects) DataContext;
            viewmodelTopics.IsDetailsVisible = !viewmodelTopics.IsDetailsVisible;

            var button = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
            if (viewmodelTopics.IsDetailsVisible)
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