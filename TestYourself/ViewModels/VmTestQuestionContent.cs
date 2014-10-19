using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TC.CustomControls;
using TC.CustomControls.MediaViewer;
using TestYourself.Helpers;
using TestYourself.Model;
using TestYourself.ResourceDictionaries;

namespace TestYourself.ViewModels
{
	public class VmTestQuestionContent : VmPage, IThumbnailedContent, INeedAcknowledgement
	{
		public int Index { get; set; }
		private readonly VmPopupHostingPage vmPopupHostingPage;

		public enum States
		{
			NotAnsweredYet,
			AnsweredCorrectly,
			AnsweredInCorrectly
		}

		private const string PropertyNameQuestionAndAnswer = "Question";
		private const string PropertyState = "State";

		public VmTestQuestionContent(VmPopupHostingPage vmPopupHostingPage, int index)
		{
			Index = index;
			this.vmPopupHostingPage = vmPopupHostingPage;
			SelectedAnswers = new ObservableCollection<object>();
			CommandShowImage = new RelayCommand(ShowImage);
		}

		private void ShowImage(object image)
		{
			if (vmPopupHostingPage.IsPopupOpened)
				throw new Exception("Unexpected");

			vmPopupHostingPage.SetContentAsPopup(image, MessageBoxResources.Instance.ImageDataTemplate);
		}

		public ICommand CommandShowImage { get; set; }

		private ObservableCollection<object> selectedAnswers;
		public ObservableCollection<object> SelectedAnswers
		{
			get { return selectedAnswers; }
			set
			{
				selectedAnswers = value;
				UpdateIsAcknowledged();
				InvokePropertyChanged("SelectedAnswers");
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
		private bool isAcknowledged;

		public bool IsResultVisible
		{
			get { return isResultVisible; }
			set
			{
				isResultVisible = value;
				InvokePropertyChanged("IsResultVisible");

				if (isResultVisible)
					ValidateAnswers();
				
				OnResultVisibilityChanged();
			}
		}


		private void ValidateAnswers()
		{
			if ((selectedAnswers != null) && selectedAnswers.Count > 0)
			{
				State = (AreAnswersCorrect(selectedAnswers.Cast<Answer>()))
					? States.AnsweredCorrectly
					: States.AnsweredInCorrectly;
			}
			else
			{
				State = States.NotAnsweredYet;
			}

			UpdateIsAcknowledged();
		}

		private void UpdateIsAcknowledged()
		{
			IsAcknowledged = SelectedAnswers != null && SelectedAnswers.Count > 0;
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
