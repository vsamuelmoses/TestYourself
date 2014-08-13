using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TC.CustomControls.MediaViewer;
using TestYourself.Helpers;
using TestYourself.Model;
using TestYourself.ResourceDictionaries;

namespace TestYourself.ViewModels
{
	public class VmQuestionContent : VmPage, IThumbnailedContent
	{
		private readonly VmTopicQuestions vmTopicQuestions;

		public enum States
		{
			NotAnsweredYet,
			AnsweredCorrectly,
			AnsweredInCorrectly
		}

		private const string PropertyNameQuestionAndAnswer = "Question";
		private const string PropertyState = "State";

		public VmQuestionContent(VmTopicQuestions vmTopicQuestions)
		{
			this.vmTopicQuestions = vmTopicQuestions;
			SelectedAnswers = new ObservableCollection<object>();
			SelectedAnswers.CollectionChanged += SelectedAnswers_CollectionChanged;
			CommandShowImage = new RelayCommand(ShowImage);
		}

		private void ShowImage(object image)
		{
			if(vmTopicQuestions.IsPopupOpened)
				throw new Exception("Unexpected");

			vmTopicQuestions.SetContentAsPopup(image, MessageBoxResources.Instance.ImageDataTemplate);
		}

		public ICommand CommandShowImage { get; set; }

		void SelectedAnswers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			IsResultVisible = false;
		}


		private ObservableCollection<object> selectedAnswers;
		public ObservableCollection<object> SelectedAnswers
		{
			get { return selectedAnswers; }
			set
			{
				selectedAnswers = value;
				InvokePropertyChanged("SelectedAnswers");
				
				IsResultVisible = false;
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
				else
					State = States.NotAnsweredYet;
			}
		}


		private void ValidateAnswers()
		{
			question.Stats.NumberOfHits++;

			if ((selectedAnswers != null) && (AreAnswersCorrect(selectedAnswers.Cast<Answer>())))
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
