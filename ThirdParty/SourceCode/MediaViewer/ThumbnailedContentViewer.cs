/* 
	Copyright (c) 2012 - 2013 Microsoft Corporation.  All rights reserved.
	Use of this sample source code is subject to the terms of the Microsoft license 
	agreement under which you licensed this sample source code and is provided AS-IS.
	If you did not accept the terms of the license agreement, you are not authorized 
	to use this sample source code.  For the terms of the license, please see the 
	license agreement between you and Microsoft.
  
	To see all Code Samples for Windows Phone, visit http://code.msdn.microsoft.com/wpapps
  
*/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TC.CustomControls.MediaViewer
{
	/// <summary>
	/// Knowns how to display an IThumbnailedImage, picking the thumbnail or 
	/// full resolution image to display based on the container size.  The
	/// IThumbnailedImage to display should be assigned to the DataContext
	/// property.
	/// </summary>
	public class ThumbnailedContentViewer : Control
	{
		private ContentControl contentControl = null;
		private FrameworkElement _placeholder = null;

		public ThumbnailedContentViewer()
		{
			DefaultStyleKey = typeof(ThumbnailedContentViewer);

			// Register for DataContext change notifications
			//
			DependencyProperty dataContextDependencyProperty = System.Windows.DependencyProperty.RegisterAttached("DataContextProperty", typeof(object), typeof(FrameworkElement), new System.Windows.PropertyMetadata(OnDataContextPropertyChanged));
			SetBinding(dataContextDependencyProperty, new Binding());
		}

		public static readonly DependencyProperty DataTemplateProperty = DependencyProperty.Register(
			"DataTemplate", typeof (DataTemplate), typeof (ThumbnailedContentViewer), new PropertyMetadata(default(DataTemplate)));

		public DataTemplate DataTemplate
		{
			get { return (DataTemplate) GetValue(DataTemplateProperty); }
			set { SetValue(DataTemplateProperty, value); }
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			contentControl = GetTemplateChild("Content") as ContentControl;
			_placeholder = GetTemplateChild("Placeholder") as FrameworkElement;

			if (contentControl != null) 
				contentControl.ContentTemplate = this.DataTemplate;
			
			BeginLoadingFullResolutionImage();
			HidePlaceholder();
		}

		private static void OnDataContextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var mediaItemViewer = (ThumbnailedContentViewer)d;

			var newContent = e.NewValue as IThumbnailedContent;

			if (e.NewValue != null)
			{
				mediaItemViewer.ShowPlaceholder();
				mediaItemViewer.BeginLoadingFullResolutionImage();
				mediaItemViewer.HidePlaceholder();
			}
			else
			{
				mediaItemViewer.contentControl.Content = null;
			}
		}

		private void BeginLoadingFullResolutionImage()
		{
			if (DataContext is IThumbnailedContent == false || contentControl == null)
			{
				return;
			}

			//Tracing.Trace("MediaItemViewer.BeginLoadingFullResolutionImage()");

			contentControl.Content = ((IThumbnailedContent) DataContext).GetContent();
		}

		private void ShowPlaceholder()
		{
			if (_placeholder != null)
			{
				_placeholder.Visibility = Visibility.Visible;
			}
			if (contentControl != null)
			{
				contentControl.Visibility = Visibility.Collapsed;
			}
		}

		private void HidePlaceholder()
		{
			if (contentControl != null)
			{
				contentControl.Visibility = Visibility.Visible;
			}
			if (_placeholder != null)
			{
				_placeholder.Visibility = Visibility.Collapsed;
			}
		}
	}
}


