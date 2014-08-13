using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using TestYourself.Model;

namespace TestYourself.Helpers
{
    public class DatabaseHelper
    {
        private static void MoveDatabaseToIsolatedStorage(string dbName)
        {
            var dBConnectionString = "Data Source=isostore:/" + dbName;

			using (var db = new TopicsDataContext(dBConnectionString))
			{
				if (db.DatabaseExists())
				{
					return;
				}
			}

            // Obtain the virtual store for the application.
            IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication();

            // Create a stream for the file in the installation folder.
            using (Stream input = Application.GetResourceStream(new Uri(dbName, UriKind.Relative)).Stream)
            {
                // Create a stream for the new file in isolated storage.
                using (IsolatedStorageFileStream output = iso.CreateFile(dbName))
                {
                    // Initialize the buffer.
                    byte[] readBuffer = new byte[input.Length];
                    int bytesRead = -1;

                    // Copy the file from the installation folder to isolated storage. 
                    while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                    {
                        output.Write(readBuffer, 0, bytesRead);
                    }
                }
            }

            //MoveDirectoryToIsolatedStorage("Databases//DrivingTheory//Images");
        }

        public static void MoveAllDatabasesToIsolatedStorage()
        {
            Databases = new List<string>();
            Databases.Add(TYDrivingTheory);

            Databases.ForEach(MoveDatabaseToIsolatedStorage);
        }

        public static List<string> Databases { get; set; }

        private const string TestYourselfLifeInUk = "TestYourselfLifeInUk.sdf";
        private const string TYDrivingTheory = "TYDrivingTheroy.sdf";
    }
}
