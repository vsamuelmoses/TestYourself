using System;
using System.Collections.ObjectModel;
using System.Linq;
using TestYourself.ViewModel;

namespace TestYourself.Model
{
	public class Subject : ObservableModel
	{
		// LINQ to SQL data context for the local database.
		private TopicsDataContext topicsDb;
		private double percentageWorked;
		private double successPercentage;
		  
		public void LoadFrom(string dataBaseName)
		{
			string dBConnectionString = "Data Source=isostore:/" + dataBaseName;
			//Name = dataBaseName;

			topicsDb = new TopicsDataContext(dBConnectionString);

			Name = topicsDb.Databases.ToList()[0].Name;

			LoadCollectionsFromDatabase();
			CreateStatasticsTable();

			CalculatePercentageWorked();
			CalculateSuccessPercentage();
		}

		public void SaveChanges()
		{
			topicsDb.SubmitChanges();
			//LoadCollectionsFromDatabase();
		}

		public void ResetProgress()
		{
			foreach (var topic in Topics)
			{
				topic.ResetProgress();
			}
		}

		private void CreateStatasticsTable()
		{
			try
			{
				var tablecount = topicsDb.QuestionStats.Count();
			}
			catch (Exception)
			{
				topicsDb.CreateTable<TopicStats>();
				topicsDb.CreateTable<QuestionStats>();

				PopulateTopicStatsTable();
				PopulateQuestionStatsTable();

				SaveChanges();
			}
		}

		private void PopulateTopicStatsTable()
		{
			foreach (var topic in Topics)
				CreateStatsForTopic(topic);
		}

		private void CreateStatsForTopic(Topic topic)
		{
			foreach (var subTopic in topic.SubTopics)
				CreateStatsForTopic(subTopic);

			topicsDb.TopicStats.InsertOnSubmit(new TopicStats(){AssociatedTopic = topic, SuccessRate = 100});
		}

		private void PopulateQuestionStatsTable()
		{
			foreach (var topic in Topics)
				CreateStatsForQuestionsInTopic(topic);
		}

		private void CreateStatsForQuestionsInTopic(Topic topic)
		{
			foreach (var subtopic in topic.SubTopics)
				CreateStatsForQuestionsInTopic(subtopic);

			foreach (var question in topic.Questions)
				topicsDb.QuestionStats.InsertOnSubmit(new QuestionStats() { AssociatedQuestion = question });
		}

		// Query database and load the collections and list used by the pivot pages.
		public void LoadCollectionsFromDatabase()
		{
			IQueryable<Topic> topicsInDb = from Topic topic in topicsDb.Topics where topic.parentTopicNumber == null select topic;

			// Query the database and load all to-do items.
			Topics = new ObservableCollection<Topic>(topicsInDb);

			foreach (var topic in Topics)
			{
				topic.AssociatedSubject = this;
			}
		}

		public void CalculatePercentageWorked()
		{
			double totalPercentage = Topics.Sum(subtopic => subtopic.Stats.ProgressPercentage);
			PercentageWorked = totalPercentage / Topics.Count;

			PercentageWorked = Math.Round(PercentageWorked, 2);
		}

		public void CalculateSuccessPercentage()
		{
			double totalPercentage = Topics.Sum(subtopic => subtopic.Stats.SuccessRate);
			SuccessPercentage = totalPercentage / Topics.Count;

			SuccessPercentage = Math.Round(SuccessPercentage, 2);
		}

		public string Name { get; private set; }
		public ObservableCollection<Topic> Topics { get; private set; }
		public double PercentageWorked
		{
			get { return percentageWorked; }
			set
			{
				percentageWorked = value;
				InvokePropertyChanged("PercentageWorked");
			}

		}
		public int TotalNumberOfQuestions { get; private set; }
		public int TotalNumberOfTopics { get { return Topics.Count(); } }
		public double SuccessPercentage
		{
			get { return successPercentage; }
			set
			{
				successPercentage = value;
				InvokePropertyChanged("SuccessPercentage");
			}
		}
	}
}
