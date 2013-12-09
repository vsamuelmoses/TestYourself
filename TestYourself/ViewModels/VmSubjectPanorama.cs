using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Navigation;
using TestYourself.Helpers;
using TestYourself.Model;
using TestYourself.ViewModel;

namespace TestYourself.ViewModels
{
    public class VmSubjectPanorama : VmPage
    {
        private readonly NavigationService navigationService;

        public VmSubjectPanorama(Subject subject, NavigationService navigationService)
        {
            this.navigationService = navigationService;
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

        private Topic selectedTopic;
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

                navigationService.Navigate(new Uri("/Views/TopicsInfoPage.xaml", UriKind.Relative));
            }
        }
        
        private ObservableCollection<Topic> topics;
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

        private bool isTopicsDetailsVisible;
        public bool IsTopicsDetailsVisible
        {
            get { return isTopicsDetailsVisible; }
            set
            {
                isTopicsDetailsVisible = value;
                InvokePropertyChanged("IsTopicsDetailsVisible");
            }
        }
        
        private int totalNumberOfQuestions;
        public int TotalNumberOfQuestions
        {
            get { return totalNumberOfQuestions; }
            set
            {
                totalNumberOfQuestions = value;
                InvokePropertyChanged("TotalNumberOfQuestions");
            }
        }

        private double successPercentage;
        private double progress;

        public double SuccessPercentage
        {
            get { return ParentSubject.SuccessPercentage; }
        }

        public double Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                InvokePropertyChanged("Progress");
            }
        }

        public RelayCommand CommandResetProgress { get; set; }
    }
}
