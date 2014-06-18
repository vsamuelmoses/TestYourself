using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace TestYourself.Controls
{
	public class SmartListBox : ListBox
	{
		public static readonly DependencyProperty BindableSelectedItemsProperty = DependencyProperty.Register(
			"BindableSelectedItems", typeof (IList), typeof (SmartListBox), new PropertyMetadata(default(IList)));

		public IList BindableSelectedItems
		{
			get { return (IList) GetValue(BindableSelectedItemsProperty); }
			set { SetValue(BindableSelectedItemsProperty, value); }
		}
		
		public SmartListBox()
		{
			SelectionChanged += SmartListBoxSelectionChanged;
			//BindableSelectedItemsProperty = DependencyProperty.Register("BindableSelectedItems", typeof(ObservableCollection<object>), typeof(SmartListBox), new PropertyMetadata(new ObservableCollection<object>()));
		}

		void SmartListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//var items = new List<object>();
			//items.AddRange(SelectedItems.Cast<object>());
			//BindableSelectedItems = items;
			BindableSelectedItems.Clear();
			foreach (var item in SelectedItems)
			{
				BindableSelectedItems.Add(item);
			}

		}

	}
}
