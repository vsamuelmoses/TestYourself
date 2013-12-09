using System.Data.Linq;
using Microsoft.Phone.Data.Linq;

namespace TestYourself.Model
{
    public class TopicsDataContext : DataContext
    {
        // Pass the connection string to the base class.
        public TopicsDataContext(string connectionString)
            : base(connectionString)
        { }


        public void CreateTable<T>()
        {
            DatabaseSchemaUpdater dbUpdater = this.CreateDatabaseSchemaUpdater();
            dbUpdater.AddTable<T>();
            dbUpdater.Execute();
        }

        public Table<Database> Databases;
        public Table<Topic> Topics;
        public Table<TopicStats> TopicStats;
        public Table<QuestionStats> QuestionStats;
    }
}
