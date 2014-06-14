using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TC.CustomControls.MediaViewer;
using TestYourself.Helpers;
using TestYourself.Model;
using TestYourself.ViewModel;

namespace TestYourself.ViewModels
{
	public class VmQuestionContent : VmPage, IThumbnailedContent
	{
		public enum States
		{
			NotAnsweredYet,
			AnsweredCorrectly,
			AnsweredInCorrectly
		}

		private const string PropertyNameQuestionAndAnswer = "Question";
		private const string PropertyState = "State";

		private List<Answer> selectedAnswers;
		public IList SelectedAnswers
		{
			get { return selectedAnswers; }
			set
			{
				selectedAnswers = new List<Answer>();

				if (value == null)
					return;

				foreach (var answer in value)
				{
					selectedAnswers.Add((Answer)answer);
				}

				IsResultVisible = false;
				State = States.NotAnsweredYet;
			}
		}

		private Question question;
		public Question Question
		{
			get { return question; }
			set
			{
				if (question == value)
					return;

				question = value;
				InvokePropertyChanged(PropertyNameQuestionAndAnswer);
				State = States.NotAnsweredYet;
				IsResultVisible = false;
			}
		}

		private Topic topic;
		public Topic Topic
		{
			get { return topic; }
			set
			{
				if (topic == value)
					return;

				topic = value;

				if (topic.Questions.Count <= 0)
					return;

				var settings = AppSettings.Instance.GetTopicSettings(topic);

				if (settings.LastVisitedQuestionNumber.HasValue)
					Question = topic.Questions.Single(q => q.QuestionNumber == settings.LastVisitedQuestionNumber.Value);
				else
					Question = topic.Questions[0];

				TitleName = Topic.Name;
			}
		}

		private States state;
		public States State
		{
			get { return state; }
			set
			{
				if (state == value)
					return;

				state = value;
				InvokePropertyChanged(PropertyState);
			}
		}

		private bool isResultVisible;
		public bool IsResultVisible
		{
			get { return isResultVisible; }
			set
			{
				isResultVisible = value;
				InvokePropertyChanged("IsResultVisible");

				if(isResultVisible)
					ValidateAnswers();
			}
		}

		private RelayCommand commandGetNextQuestion;
		public ICommand CommandGetNextQuestion
		{
			get
			{
				return commandGetNextQuestion ??
					   (commandGetNextQuestion = new RelayCommand(param =>
					   {
						   if (Topic.Questions.Count > Topic.Questions.IndexOf(Question) + 1)
							   Question = Topic.Questions[Topic.Questions.IndexOf(Question) + 1];

					   },
																  param => true));
			}
		}

		private RelayCommand commandGetPreviousQuestion;
		public ICommand CommandGetPreviousQuestion
		{
			get
			{
				return commandGetPreviousQuestion ??
					   (commandGetPreviousQuestion = new RelayCommand(param =>
					   {
						   if ((Topic.Questions.Count > Topic.Questions.IndexOf(Question) - 1) &&
							   (Topic.Questions.IndexOf(Question) - 1) >= 0)
							   Question = Topic.Questions[Topic.Questions.IndexOf(Question) - 1];
					   },
																  param => true));
			}
		}

		private RelayCommand commandCheckAnswer;

		public ICommand CommandCheckAnswer
		{
			get
			{
				return commandCheckAnswer ??
					   (commandCheckAnswer = new RelayCommand(param =>
					   {
						   ValidateAnswers();
						   IsResultVisible = true;
					   },
							param => true));
			}
		}

		private void ValidateAnswers()
		{
			question.Stats.NumberOfHits++;

			if ((selectedAnswers != null) && (AreAnswersCorrect(selectedAnswers)))
			{
				Question.Stats.NumberOfHitsCorrectlyAnswered++;
				State = States.AnsweredCorrectly;
			}
			else
			{
				State = States.AnsweredInCorrectly;
			}

			question.AssociatedTopic.UpdateStats();
		}

		private bool AreAnswersCorrect(IEnumerable<Answer> answers)
		{
			var correctAnswers = Question.Answers.Where(ans => ans.IsCorrect).ToList();

			if (answers.Count() != correctAnswers.Count)
				return false;

			return correctAnswers.All(answers.Contains);
		}

		public object GetContent()
		{
			return this;
		}
	}
}
