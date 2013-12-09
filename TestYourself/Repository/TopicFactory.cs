using TestYourself.Model;

namespace TestYourself.Repository
{/*
    public static class TopicsFactory
    {

        public static Topic GetTopic()
        {
            var plab = new Topic("PLAB");

            var topic1 = new Topic("TOPIC 1");
            var topic2 = new Topic("TOPIC 2");
            var topic3 = new Topic("TOPIC 3");
            var topic4 = new Topic("TOPIC 4");
            var topic5 = new Topic("TOPIC 5");

            var topic11 = new Topic("TOPIC 11");
            var topic12 = new Topic("TOPIC 12");
            var topic13 = new Topic("TOPIC 13");
            var topic14 = new Topic("TOPIC 14");
            var topic15 = new Topic("TOPIC 15");

            var topic21 = new Topic("TOPIC 21");
            var topic31 = new Topic("TOPIC 31");
            var topic41 = new Topic("TOPIC 41");
            var topic51 = new Topic("TOPIC 51");


            topic1.Topics.Add(topic11);
            topic1.Topics.Add(topic12);
            topic1.Topics.Add(topic13);
            topic1.Topics.Add(topic14);
            topic1.Topics.Add(topic15);

            topic2.Topics.Add(topic21);
            topic3.Topics.Add(topic31);
            topic4.Topics.Add(topic41);
            topic5.Topics.Add(topic51);


            plab.Topics.Add(topic1);
            plab.Topics.Add(topic2);
            plab.Topics.Add(topic3);
            plab.Topics.Add(topic4);
            plab.Topics.Add(topic5);

            return plab;
        }

        private static int Index = 1;

        private static Topic GetTopic(string name, int numberOfSubTopics)
        {

            Topic mainTopic = new Topic(name);

            for (int i = 0; i < numberOfSubTopics; i++)
            {
                var topic = new Topic(name + " Topic " + Index + "." + i);
                mainTopic.Topics.Add(topic);
            }

            return mainTopic;
        }



    }
  * 
  */ 
}
