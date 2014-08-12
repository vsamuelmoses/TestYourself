using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace TestYourself.Model
{
    [Table(Name="Topics")]
    public class Topic : INotifyPropertyChanged, INotifyPropertyChanging
    {

        #region Column: TopicNumber 
        // Define ID: private field, public property, and database column.
        private int topicNumber;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int TopicNumber
        {
            get { return topicNumber; }
            set
            {
                if (topicNumber != value)
                {
                    NotifyPropertyChanging("TopicNumber");
                    topicNumber = value;
                    NotifyPropertyChanged("TopicNumber");
                }
            }
        }

        #endregion

        #region Column: Name
        // Define item name: private field, public property, and database column.
        private string name;

        [Column]
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    NotifyPropertyChanging("Name");
                    name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
        #endregion

        #region Column: ParentTopicNumber

        // Internal column for the associated ToDoCategory ID value
        [Column(CanBeNull=true)]
        internal int? parentTopicNumber;

        // Entity reference, to identify the ToDoCategory "storage" table
        private EntityRef<Topic> parentTopic;

        // Association, to describe the relationship between this key and that "storage" table
        [Association(Storage = "parentTopic", ThisKey = "parentTopicNumber", OtherKey = "TopicNumber", IsForeignKey = true)]
        public Topic ParentTopic
        {
            get { return parentTopic.Entity; }
            set
            {
                NotifyPropertyChanging("ParentTopic");
                parentTopic.Entity = value;

                if (value != null)
                {
                    parentTopicNumber = value.TopicNumber;
                }

                NotifyPropertyChanging("ParentTopic");
            }
        }


        #endregion
        

        #region SubTopics

        // Define the entity set for the collection side of the relationship.
        private EntitySet<Topic> subTopics;

        [Association(Storage = "subTopics", OtherKey = "parentTopicNumber", ThisKey = "TopicNumber")]
        public EntitySet<Topic> SubTopics
        {
            get { return this.subTopics; }
            set { this.subTopics.Assign(value); }
        }

        #endregion
        
        #region Questions
        // Define the entity set for the collection side of the relationship.
        private EntitySet<Question> questions;

        [Association(Storage = "questions", OtherKey = "topicNumber", ThisKey = "TopicNumber")]
        public EntitySet<Question> Questions
        {
            get { return this.questions; }
            set
            {
                this.questions.Assign(value);

            }
        }
        #endregion

        #region KeyPoints
        // Define the entity set for the collection side of the relationship.
        private EntitySet<KeyPoint> keyPoints;

        [Association(Storage = "keyPoints", OtherKey = "topicNumber", ThisKey = "TopicNumber")]
        public EntitySet<KeyPoint> KeyPoints
        {
            get { return this.keyPoints; }
            set { this.keyPoints.Assign(value); }
        }

        #endregion

        #region ImageData
        // Define the entity set for the collection side of the relationship.
        private EntitySet<ImageData> imageData;

        [Association(Storage = "imageData", OtherKey = "topicNumber", ThisKey = "TopicNumber")]
        public EntitySet<ImageData> ImageData
        {
            get { return imageData; }
            set { imageData.Assign(value); }
        }

        #endregion



        private EntityRef<TopicStats> stats;

        [Association(Storage = "stats", OtherKey = "topicNumber", ThisKey = "TopicNumber")]
        public TopicStats Stats
        {
            get { return stats.Entity; }
            set { stats.Entity = value; }
        }

        // Assign handlers for the add and remove operations, respectively.
        public Topic()
        {
            questions = new EntitySet<Question>();
            keyPoints = new EntitySet<KeyPoint>();
            subTopics = new EntitySet<Topic>();
        }

        public void UpdateStats()
        {
            UpdateProgress();
            UpdateSuccessPercentage();
        }

        public int GetTotalNumberOfQuestions()
        {
            var totalQuestions = 0;
            if((SubTopics != null) && (SubTopics.Count > 0))
                totalQuestions += SubTopics.Sum(topic => topic.TotalNumberOfQuestions);

            totalQuestions += Questions.Count;
            return totalQuestions;
        }

        private void UpdateSuccessPercentage()
        {
            if (questions.Count > 0)
            {
                var questionsAnsweredAtleastOnce = from q in questions where q.Stats.NumberOfHits > 0 select q;

                if (questionsAnsweredAtleastOnce.Count() <= 0)
                    Stats.SuccessRate = 100;
                else
                    Stats.SuccessRate = questionsAnsweredAtleastOnce.Sum(question => question.SuccessPercentage) / questionsAnsweredAtleastOnce.Count();
            }
            else
            {
                double totalPercentage = SubTopics.Sum(topic => topic.Stats.SuccessRate);
                Stats.SuccessRate = totalPercentage / SubTopics.Count;
            }

            if (ParentTopic != null)
                ParentTopic.UpdateStats();
            else
                AssociatedSubject.CalculateSuccessPercentage();

            Stats.SuccessRate = Math.Round(Stats.SuccessRate, 2);
        }

        private void UpdateProgress()

        {
            if (questions.Count > 0)
            {
                var questionsAnsweredAtleastOnce = from q in questions where q.Stats.NumberOfHits > 0 select q;
                Stats.ProgressPercentage = ((double)questionsAnsweredAtleastOnce.Count() * 100) / questions.Count;
            }
            else
            {
                double totalPercentage = SubTopics.Sum(topic => topic.Stats.ProgressPercentage);
                Stats.ProgressPercentage = totalPercentage / SubTopics.Count;
            }

            if (ParentTopic != null)
                ParentTopic.UpdateStats();
            else
                AssociatedSubject.CalculatePercentageWorked();

            Stats.ProgressPercentage = Math.Round(Stats.ProgressPercentage, 2);
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion

        public int TotalNumberOfQuestions
        {
            get
            {
                if (!totalNumberOfQuestions.HasValue)
                    totalNumberOfQuestions = GetTotalNumberOfQuestions();

                return totalNumberOfQuestions.Value;
            }
        }

        public Subject AssociatedSubject { get; set; }
        
        private string GetPath(string delimiter)
        {
            string topicPath = Name;
            var parent = ParentTopic;
            
            while(parent != null)
            {
                topicPath = parent.Name + delimiter + topicPath;
                
                if(parent.ParentTopic == null)
                    topicPath = parent.AssociatedSubject.Name + delimiter + topicPath;
                
                parent = parent.ParentTopic;
            }

            return topicPath;
        }

        private string path;
        private int? totalNumberOfQuestions;

        public string Path
        {
            get
            {
                if (string.IsNullOrEmpty(path))
                {
                    path = GetPath("//");
                }

                return path;
            }
        }

        public void ResetProgress()
        {
            foreach(var topic in SubTopics)
                topic.ResetProgress();

            foreach(var question in Questions)
                question.Stats.Reset();

            UpdateStats();
        }
    }
}
