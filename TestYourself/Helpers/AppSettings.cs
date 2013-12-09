using System;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Reflection;
using System.Runtime.Serialization;
using TestYourself.Model;

namespace TestYourself.Helpers
{
    public class AppSettings
    {
        private static volatile AppSettings instance;
        private static readonly object SyncRoot = new Object();
        private readonly IsolatedStorageSettings appSettings;

        /// <summary>
        /// Constructor that gets the application settings.
        /// </summary>
        private AppSettings()
        {
            try
            {
                // Get the settings for this application.
                appSettings = IsolatedStorageSettings.ApplicationSettings;

            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception while using IsolatedStorageSettings: " + e.ToString());
            }
        }

        /// <summary>
        /// Update a setting value for our application. If the setting does not
        /// exist, then add the setting.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddOrUpdateValue(string Key, Object value)
        {
            bool valueChanged = false;

            // If the key exists
            if (appSettings.Contains(Key))
            {
                // If the value has changed
                if (appSettings[Key] != value)
                {
                    // Store the new value
                    appSettings[Key] = value;
                    valueChanged = true;
                }
            }
            // Otherwise create the key.
            else
            {
                appSettings.Add(Key, value);
                valueChanged = true;
            }

            return valueChanged;
        }


        /// <summary>
        /// Get the current value of the setting, or if it is not found, set the 
        /// setting to the default setting.
        /// </summary>
        /// <typeparam name="valueType"></typeparam>
        /// <param name="Key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public valueType GetValueOrDefault<valueType>(string Key, valueType defaultValue)
        {
            valueType value;

            // If the key exists, retrieve the value.
            if (appSettings.Contains(Key))
            {
                value = (valueType)appSettings[Key];
            }
            // Otherwise, use the default value.
            else
            {
                value = defaultValue;
            }

            return value;
        }

        /// <summary>
        /// Save the settings.
        /// </summary>
        public void Save()
        {
            appSettings.Save();
        }



        public void Print()
        {
            Debug.WriteLine("App Settings on " + DateTime.Now);
            foreach (var setting in appSettings)
            {
                Debug.WriteLine(setting.Key, setting.Value);
            }
        }


        public TopicSettings GetTopicSettings(Topic topic)
        {
            return GetValueOrDefault(topic.Path, new TopicSettings(topic));
        }

        public void SetTopicSettings(Topic topic, TopicSettings settings)
        {
            AddOrUpdateValue(topic.Path, settings);
            Save();
        }

        public static string GetVersionNumber()
        {
            var asm = Assembly.GetExecutingAssembly();
            var parts = asm.FullName.Split(',');
            return parts[1].Split('=')[1];
        }

        public static AppSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                            instance = new AppSettings();
                    }
                }

                return instance;
            }
        }

    }

    
    [DataContract]
    public class TopicSettings
    {
        public TopicSettings(Topic topic)
        {
            TopicPath = topic.Path;
            LastVisitedQuestionNumber = null;
        }

        [DataMember]
        public int? LastVisitedQuestionNumber { get; set; }

        [DataMember]
        public string TopicPath { get; set; }
    }
}
