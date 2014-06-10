using TestYourself.ViewModels;

namespace TestYourself.ViewModel
{
    public class VmLocator
    {
        private static VmLocator instance;

        private VmLocator()
        {
            VmQuestion = new VmQuestion();
            VmQuestionNew = new ViewModels.VmQuestion();
        }

        public VmQuestion VmQuestion { get; private set; }
        public ViewModels.VmQuestion VmQuestionNew { get; private set; }

        private VmSubjects vmSubjects;

        public VmSubjects VmSubjects
        {
            get { return vmSubjects ?? (vmSubjects = new VmSubjects()); }
        }


        private VmTopics vmTopics;
        public VmTopics VmTopics
        {
            get
            {
                //vmTopics = vmTopics == null ? new VmTopics(VmSubjects.SelectedSubject) : new VmTopics(vmTopics.SelectedTopic);
                return vmTopics;
            }
            set { vmTopics = value; }
        }


        public static VmLocator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VmLocator();
                }
                return instance;
            }
        }

        public VmSubjectPanorama VmSubjectPanorama { get; set; }
        public VmTopicsInfo VmTopicsInfo { get; set; }
		public VmTopicQuestions VmTopicQuestions{ get; set; }
    }
}
