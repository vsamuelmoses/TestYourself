using System.Windows.Navigation;
using TestYourself.Helpers;
using TestYourself.Model;

namespace TestYourself.ViewModels
{
	public class VmTopicsPanorama : VmTopicsHostBase
	{
		public Topic Topic { get; set; }

		public VmTopicsPanorama(Topic topic, NavigationService navigationService)
			: base(navigationService)
		{
			Topic = topic;
			CommandResetProgress = new RelayCommand(notUsed => Topic.ResetProgress());
		}

		public override double SuccessPercentage
		{
			get { return Topic.Stats.SuccessRate; }
		}

		public override sealed RelayCommand CommandResetProgress { get; set; }
	}
}
