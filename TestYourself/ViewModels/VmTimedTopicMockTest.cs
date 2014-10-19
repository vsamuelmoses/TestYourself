using System;
using System.Windows.Navigation;
using System.Windows.Threading;
using TestYourself.Model;

namespace TestYourself.ViewModels
{
	public class VmTimedTopicMockTest : VmTimedMockTest
	{
		public Topic Topic { get; set; }
		public DispatcherTimer Timer { get; set; }

		private readonly TimeSpan timeTickInterval = new TimeSpan(0, 0, 0, 1);
		public VmTimedTopicMockTest(Topic topic, NavigationService navigationService) 
			: base(navigationService)
		{
			Topic = topic;
			InitialiseQuestions();
		}

		private void InitialiseQuestions()
		{
			Questions.Clear();
			var random = new Random();

			for (int i = 0; i < NumberOfQuestions; i++)
			{
				var questionIndex = random.Next(0, Topic.Questions.Count - 1);
				Questions.Add(new VmTestQuestionContent(this, i + 1) { Question = Topic.Questions[questionIndex] });
			}
		}


		public override void Start()
		{
			TimeLeft = TimeLimit;
			Timer = new DispatcherTimer {Interval = timeTickInterval};
			Timer.Tick += Timer_Tick;
			Timer.Start();
			State = TestStatus.InProgress;
		}

		void Timer_Tick(object sender, EventArgs e)
		{
			TimeLeft = TimeLeft.Subtract(timeTickInterval);

			if (TimeLeft.TotalSeconds <= 0)
			{
				Finish();
			}
		}

		public override void Pause()
		{
			throw new NotImplementedException();
		}

		public override void Finish()
		{
			Timer.Stop();
			Timer.Tick -= Timer_Tick;

			State = TestStatus.Completed;
		}

		public override void Resume()
		{
			throw new NotImplementedException();
		}
	}
}
