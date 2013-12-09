using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace TestYourself.Model
{
    [Table(Name = "DbInfo")]
    public class Database : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private string name;

        [Column]
        public string Name
        {
            get { return name; }
            set
            {
                if (value == name)
                    return;

                NotifyPropertyChanging("Name");
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private double version;

        [Column]
        public double Version
        {
            get { return version; }
            set
            {
                if (value == version)
                    return;

                NotifyPropertyChanging("Version");
                version = value;
                NotifyPropertyChanged("Version");
            }
        }

        private DateTime dateCreated;

        [Column]
        public DateTime DateCreated
        {
            get { return dateCreated; }
            set
            {
                if (value == dateCreated)
                    return;

                NotifyPropertyChanging("DateCreated");
                dateCreated = value;
                NotifyPropertyChanged("DateCreated");
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
