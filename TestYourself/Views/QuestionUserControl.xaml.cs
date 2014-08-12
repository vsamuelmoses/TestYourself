using System.Windows;
using System.Windows.Controls;

namespace TestYourself.Views
{
	public partial class QuestionUserControl : UserControl
	{
		public QuestionUserControl()
		{
			InitializeComponent();
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			
		}

		private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selectedItems = AnswerChoiceListBox.SelectedItems;

		}
	}
}
