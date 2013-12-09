using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace TestYourself.Model
{
    [Table(Name = "QuestionStats")]
    public class QuestionStats : INotifyPropertyChanged, INotifyPropertyChanging
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
        private int numberOfHits;

        [Column]
        public int NumberOfHits
        {
            get { return numberOfHits; }
            set
            {
                if (numberOfHits != value)
                {
                    NotifyPropertyChanging("NumberOfHits");
                    numberOfHits = value;
                    NotifyPropertyChanged("NumberOfHits");
                }
            }
        }

        // Define item name: private field, public property, and database column.
        private int numberOfHitsCorrectlyAnswered;

        [Column]
        public int NumberOfHitsCorrectlyAnswered
        {
            get { return numberOfHitsCorrectlyAnswered; }
            set
            {
                if (numberOfHitsCorrectlyAnswered != value)
                {
                    NotifyPropertyChanging("NumberOfHitsCorrectlyAnswered");
                    numberOfHitsCorrectlyAnswered = value;
                    NotifyPropertyChanged("NumberOfHitsCorrectlyAnswered");
                }
            }
        }


        // Assign handlers for the add and remove operations, respectively.
        public QuestionStats()
        {
            Reset();
        }


        public void Reset()
        {
            NumberOfHits = 0;
            NumberOfHitsCorrectlyAnswered = 0;
        }

        // Internal column for the associated ToDoCategory ID value
        [Column (Name="QuestionNumber")]
        internal int questionNumber;

        // Entity reference, to identify the ToDoCategory "storage" table
        private EntityRef<Question> associatedQuestion;

        // Association, to describe the relationship between this key and that "storage" table
        [Association(Storage = "associatedQuestion", ThisKey = "questionNumber", OtherKey = "QuestionNumber", IsForeignKey = true)]
        public Question AssociatedQuestion
        {
            get { return associatedQuestion.Entity; }
            set
            {
                NotifyPropertyChanging("AssociatedQuestion");
                associatedQuestion.Entity = value;

                if (value != null)
                {
                    questionNumber = value.QuestionNumber;
                    //value.Stats = this;
                }

                NotifyPropertyChanging("AssociatedQuestion");
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
