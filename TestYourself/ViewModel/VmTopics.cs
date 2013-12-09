using System.Collections.ObjectModel;
using TestYourself.Model;

namespace TestYourself.ViewModel
{
    public class VmTopics : VmBase
    {
        public VmTopics(Topic topic)
        {
            Topics =new ObservableCollection<Topic>(topic.SubTopics);
            ParentName = topic.Name;
        }

        public VmTopics(Subject subject)
        {
            Topics = new ObservableCollection<Topic>(subject.Topics);
            ParentName = subject.Name;
//            IsTopicsDetailsVisible = true;
        }

        public string ParentName { get; set; }

        private Topic selectedTopic;
        public Topic SelectedTopic
        {
            get { return selectedTopic; }
            set
            {
                if (selectedTopic == value)
                    return;

                selectedTopic = value;
            }
        }

        // All to-do items.
        private ObservableCollection<Topic> topics;
        public ObservableCollection<Topic> Topics
        {
            get { return topics; }
            set
            {
                topics = value;
                InvokePropertyChanged("Topics");
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
    }
}
