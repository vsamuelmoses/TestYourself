using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace TestYourself.Model
{
    [Table(Name = "Images")]
    public class ImageData
    {
        // Define ID: private field, public property, and database column.
        private int imageNumber;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ImageNumber
        {
            get { return imageNumber; }
            set
            {
                if (imageNumber != value)
                {
                    NotifyPropertyChanging("imageNumber");
                    imageNumber = value;
                    NotifyPropertyChanged("imageNumber");
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


        // Define item name: private field, public property, and database column.
        private string path;

        [Column]
        public string Path
        {
            get { return path; }
            set
            {
                if (path != value)
                {
                    NotifyPropertyChanging("Path");
                    path = value;
                    NotifyPropertyChanged("Path");
                }
            }
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
