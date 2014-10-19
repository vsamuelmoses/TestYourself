using System;
using System.Windows.Navigation;

namespace TestYourself.ViewModels
{
	public abstract class VmTimedMockTest : VmMockTest
	{
		private TimeSpan timeLimit = new TimeSpan(0,0,10);
		private TimeSpan timeLeft;

		protected VmTimedMockTest(NavigationService navigationService) 
			: base(navigationService)
		{
		}

		public TimeSpan TimeLeft
		{
			get { return timeLeft; }
			set
			{
				timeLeft = value;
				InvokePropertyChanged("TimeLeft");
			}
		}

		public TimeSpan TimeLimit
		{
			get { return timeLimit; }
			set
			{
				timeLimit = value;
				InvokePropertyChanged("TimeLimit");
			}
		}
	}
}