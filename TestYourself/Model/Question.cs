using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace TestYourself.Model
{
    [Table(Name = "Questions")]
    public class Question : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // Define ID: private field, public property, and database column.
        private int questionNumber;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int QuestionNumber
        {
            get { return questionNumber; }
            set
            {
                if (questionNumber != value)
                {
                    NotifyPropertyChanging("QuestionNumber");
                    questionNumber = value;
                    NotifyPropertyChanged("QuestionNumber");
                }
            }
        }

        // Define item name: private field, public property, and database column.
        private string text;

        [Column]
        public string Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    NotifyPropertyChanging("Text");
                    text = value;
                    NotifyPropertyChanged("Text");
                }
            }
        }

        // Define the entity set for the collection side of the relationship.
        private EntitySet<Answer> answers;

        [Association(Storage = "answers", OtherKey = "questionNumber", ThisKey = "QuestionNumber")]
        public EntitySet<Answer> Answers
        {
            get { return this.answers; }
            set { this.answers.Assign(value); }
        }



        private EntityRef<QuestionStats> stats;

        [Association(Storage = "stats", OtherKey = "questionNumber", ThisKey = "QuestionNumber")]
        public QuestionStats Stats
        {
            get { return stats.Entity; }
            set { stats.Entity = value; }
        }


        public int Index
        {
            get { return AssociatedTopic.Questions.IndexOf(this) + 1; }
        }

        // Assign handlers for the add and remove operations, respectively.
        public Question()
        {
            answers = new EntitySet<Answer>();
            keyPoint = default(EntityRef<KeyPoint>);
            stats = default(EntityRef<QuestionStats>);
        }


        // Internal column for the associated ToDoCategory ID value
        [Column]
        internal int topicNumber;

        // Entity reference, to identify the ToDoCategory "storage" table
        private EntityRef<Topic> associatedTopic;

        // Association, to describe the relationship between this key and that "storage" table
        [Association(Storage = "associatedTopic", ThisKey = "topicNumber", OtherKey = "TopicNumber", IsForeignKey = true)]
        public Topic AssociatedTopic
        {
            get { return associatedTopic.Entity; }
            set
            {
                NotifyPropertyChanging("AssociatedTopic");
                associatedTopic.Entity = value;

                if (value != null)
                {
                    topicNumber = value.TopicNumber;
                }

                NotifyPropertyChanging("AssociatedTopic");
            }
        }

        // Internal column for the associated ToDoCategory ID value
        [Column(CanBeNull = true)]
        internal int? keyPointNumber;

        // Entity reference, to identify the ToDoCategory "storage" table
        private EntityRef<KeyPoint> keyPoint;

        // Association, to describe the relationship between this key and that "storage" table
        [Association(Storage = "keyPoint", ThisKey = "keyPointNumber", OtherKey = "KeyPointNumber", IsForeignKey = true)]
        public KeyPoint KeyPoint
        {
            get { return keyPoint.Entity; }
            set
            {
                NotifyPropertyChanging("KeyPoint");
                keyPoint.Entity = value;

                if (value != null)
                {
                    keyPointNumber = value.KeyPointNumber;
                }

                NotifyPropertyChanging("KeyPoint");
            }
        }

        // Internal column for the associated ToDoCategory ID value
        [Column(CanBeNull = true)]
        internal int? imageNumber;

        // Entity reference, to identify the ToDoCategory "storage" table
        private EntityRef<ImageData> imageData;

        // Association, to describe the relationship between this key and that "storage" table
        [Association(Storage = "imageData", ThisKey = "imageNumber", OtherKey = "ImageNumber", IsForeignKey = true)]
        public ImageData ImageData
        {
            get { return imageData.Entity; }
            set
            {
                NotifyPropertyChanging("ImageData");
                imageData.Entity = value;

                if (value != null)
                {
                    imageNumber = value.ImageNumber;
                }

                NotifyPropertyChanging("ImageData");
            }
        }


        public double SuccessPercentage
        {
            get
            {
                if (Stats.NumberOfHits <= 0)
                    return 0;

                return Math.Round(((double)(Stats.NumberOfHitsCorrectlyAnswered*100))/Stats.NumberOfHits, 2);
            }
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
    }
}
