using System;
using System.Windows;

namespace TestYourself.ResourceDictionaries
{
	public class MessageBoxResources
	{
		private static MessageBoxResources instance;

		private MessageBoxResources()
		{
			MessageBoxResourceDictionary = new ResourceDictionary();

			MessageBoxResourceDictionary.Source =
				new Uri("/TestYourself;component/ResourceDictionaries/MessageBoxStyles.xaml",
					UriKind.RelativeOrAbsolute);
		}


		public DataTemplate KeypointDataTemplate 
		{
			get { return (DataTemplate)MessageBoxResourceDictionary["KeypointDataTemplate"]; }
		}

		public DataTemplate ImageDataTemplate
		{
			get
			{
				return (DataTemplate)MessageBoxResourceDictionary["ImageDataTemplate"];
			}
		}


		public ResourceDictionary MessageBoxResourceDictionary { get; set; }


		public static MessageBoxResources Instance
		{
			get
			{
				//if (instance == null)
				{
					instance = new MessageBoxResources();
				}
				return instance;
			}
		}

	}
}
