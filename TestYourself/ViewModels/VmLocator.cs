namespace TestYourself.ViewModels
{
	public class VmLocator
	{
		private static VmLocator instance;

		private VmLocator()
		{
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
		public VmTopicsPanorama VmTopicsPanorama { get; set; }
	}
}
