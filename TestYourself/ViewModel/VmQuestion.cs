using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using TestYourself.Helpers;
using TestYourself.Model;

namespace TestYourself.ViewModel
{
    public class VmQuestion : VmBase
    {
        public enum States
        {
            NotAnsweredYet,
            AnsweredCorrectly, 
            AnsweredInCorrectly
        }


        private const string PropertyNameQuestionAndAnswer = "Question";
        private const string PropertyState = "State";

        private Question question;
        public Question Question
        {
            get
            {
                return question;
            }
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

                if (topic.Questions.Count <= 0)
                    return;

                var settings = AppSettings.Instance.GetTopicSettings(topic);

                if (settings.LastVisitedQuestionNumber.HasValue)
                    Question = topic.Questions.Where(q => q.QuestionNumber == settings.LastVisitedQuestionNumber.Value).Single();
                else
                    Question = topic.Questions[0];
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

        #region COMMANDS 

        RelayCommand commandGetNextQuestion;
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



        RelayCommand commandGetPreviousQuestion;
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

        RelayCommand commandCheckAnswer;
        public ICommand CommandCheckAnswer
        {
            get
            {
                return commandCheckAnswer ??
                       (commandCheckAnswer = new RelayCommand(param =>
                                                                  {
                                                                      question.Stats.NumberOfHits++;

                                                                      var answers = ((ObservableCollection<object>)param).ToList().Cast<Answer>();

                                                                      if ((answers != null) && (AreAnswersCorrect(answers)))
                                                                      {
                                                                          Question.Stats.NumberOfHitsCorrectlyAnswered++;
                                                                          State = States.AnsweredCorrectly;
                                                                      }
                                                                      else
                                                                      {
                                                                          State = States.AnsweredInCorrectly;
                                                                      }

                                                                      question.AssociatedTopic.UpdateStats();

                                                                      
                                                                  },
                                                                  param => true));
            }
        }

        private bool AreAnswersCorrect(IEnumerable<Answer> answers)
        {
            var correctAnswers = Question.Answers.Where(ans => ans.IsCorrect).ToList();

            if (answers.Count() != correctAnswers.Count)
                return false;

            return correctAnswers.All(answers.Contains);
        }

        #endregion
    }
}
