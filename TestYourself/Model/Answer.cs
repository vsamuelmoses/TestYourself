using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace TestYourself.Model
{
    [Table(Name = "Answers")]
    public class Answer : INotifyPropertyChanged, INotifyPropertyChanging
    {

        // Define ID: private field, public property, and database column.
        private int answerNumber;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int AnswerNumber
        {
            get { return answerNumber; }
            set
            {
                if (answerNumber != value)
                {
                    NotifyPropertyChanging("AnswerNumber");
                    answerNumber = value;
                    NotifyPropertyChanged("AnswerNumber");
                }
            }
        }




        // Define item name: private field, public property, and database column.
        private bool isCorrect;

        [Column]
        public bool IsCorrect
        {
            get { return isCorrect; }
            set
            {
                if (isCorrect != value)
                {
                    NotifyPropertyChanging("IsCorrect");
                    isCorrect = value;
                    NotifyPropertyChanged("IsCorrect");
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

        // Internal column for the associated ToDoCategory ID value
        [Column]
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
                }

                NotifyPropertyChanging("AssociatedQuestion");
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
