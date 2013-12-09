using System.Collections.ObjectModel;
using System.Linq;
using TestYourself.Helpers;
using TestYourself.Model;

namespace TestYourself.ViewModel
{
    public class VmSubjects : VmBase
    {
        public VmSubjects()
        {
            Subjects = new ObservableCollection<Subject>();
            DatabaseHelper.Databases.ForEach(AddSubject);
            IsDetailsVisible = false;
        }

        private void AddSubject(string dbName)
        {
            var subject = new Subject();
            subject.LoadFrom(dbName);
            Subjects.Add(subject);
        }

        public void SaveAllSubjects()
        {
            foreach (var subject in Subjects)
            {
                subject.SaveChanges();
            }
        }

        public ObservableCollection<Subject> Subjects { get; set; }
        public Subject SelectedSubject { get; set; }
        public Subject DefaultSubject { get { return Subjects.First(); } }

        private bool isDetailsVisible;
        public bool IsDetailsVisible
        {
            get { return isDetailsVisible; }
            set
            {
                isDetailsVisible = value;
                InvokePropertyChanged("IsDetailsVisible");
            }
        }
    }
}
