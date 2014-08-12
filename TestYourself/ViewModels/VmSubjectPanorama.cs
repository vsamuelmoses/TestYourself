using System.Collections.ObjectModel;
using System.Windows.Navigation;
using TestYourself.Helpers;
using TestYourself.Model;

namespace TestYourself.ViewModels
{
	public class VmSubjectPanorama : VmTopicsHostBase
	{
		private readonly NavigationService navigationService;

		public VmSubjectPanorama(Subject subject, NavigationService navigationService) 
			: base(navigationService)
		{
			ParentSubject = subject;
			CommandResetProgress = new RelayCommand(notUsed => ParentSubject.ResetProgress());
		}

		private Subject parentSubject;



		public Subject ParentSubject
		{
			get { return parentSubject; }
			set
			{
				parentSubject = value;
				InvokePropertyChanged("ParentSubject");

				Topics = new ObservableCollection<Topic>(parentSubject.Topics);
				TitleName = parentSubject.Name;
				//SuccessPercentage = parentSubject.SuccessPercentage;
				Progress = parentSubject.PercentageWorked;
			}
		}

		public override double SuccessPercentage
		{
			get { return ParentSubject.SuccessPercentage; }
		}

		public override sealed RelayCommand CommandResetProgress { get; set; }
	}
}
