using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Phone.Reactive;
using TC.CustomControls;
using TC.CustomControls.MediaViewer;
using TestYourself.Helpers;
using TestYourself.Model;
using TestYourself.ResourceDictionaries;

namespace TestYourself.ViewModels
{
	public class VmQuestionContent : VmPage, IThumbnailedContent, INeedAcknowledgement
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
				UpdateIsAcknowledged();
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
				//IsAcknowledged = (state == States.AnsweredCorrectly || state == States.AnsweredInCorrectly);
				InvokePropertyChanged(PropertyState);
			}
		}

		private bool isResultVisible;
		private bool isAcknowledged;

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

				OnResultVisibilityChanged();
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
			UpdateIsAcknowledged();
		}

		private void UpdateIsAcknowledged()
		{
			IsAcknowledged = question.Stats.NumberOfHits > 0;
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

		public bool IsAcknowledged
		{
			get { return isAcknowledged; }
			set
			{
				if (isAcknowledged == value)
					return;

				isAcknowledged = value;
				InvokePropertyChanged("IsAcknowledged");

			}
		}

		public event EventHandler ResultVisibilityChanged;

		protected virtual void OnResultVisibilityChanged()
		{
			EventHandler handler = ResultVisibilityChanged;
			if (handler != null) handler(this, EventArgs.Empty);
		}
	}
}
