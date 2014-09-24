using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace TestYourself.Model
{
    [Table(Name = "TopicStats")]
    public class TopicStats : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // Define ID: private field, public property, and database column.
        private int statNumber;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int StatNumber
        {
            get { return statNumber; }
            set
            {
                if (statNumber != value)
                {
                    NotifyPropertyChanging("StatNumber");
                    statNumber = value;
                    NotifyPropertyChanged("StatNumber");
                }
            }
        }

        // Define item name: private field, public property, and database column.
        private double? successRate;

        [Column]
        public double? SuccessRate
        {
            get { return successRate; }
            set
            {
                if (successRate != value)
                {
                    NotifyPropertyChanging("SuccessRate");
                    successRate = value;
                    NotifyPropertyChanged("SuccessRate");
                }
            }
        }

        // Define item name: private field, public property, and database column.
        private double progressPercentage;

        [Column]
        public double ProgressPercentage
        {
            get { return progressPercentage; }
            set
            {
                if (progressPercentage != value)
                {
                    NotifyPropertyChanging("ProgressPercentage");
                    progressPercentage = value;
                    NotifyPropertyChanged("ProgressPercentage");
                }
            }
        }


        // Internal column for the associated ToDoCategory ID value
        [Column(Name = "TopicNumber")]
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
                    //value.Stats = this;
                }

                NotifyPropertyChanging("AssociatedTopic");
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
