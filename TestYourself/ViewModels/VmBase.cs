using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;
using TestYourself.Annotations;

namespace TestYourself.ViewModels
{
    public abstract class ObservableModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            PropertyChanged.Raise(propertyExpression);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void InvokePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public static class NotifyPropertyChangedHelper
    {
        public static void Raise<T>(this PropertyChangedEventHandler handler, Expression<Func<T>> propertyExpression)
        {
            if (handler != null)
            {
                var body = propertyExpression.Body as MemberExpression;
                if (body == null)
                    throw new ArgumentException("'propertyExpression' should be a member expression");

                var expression = body.Expression as ConstantExpression;
                if (expression == null)
                    throw new ArgumentException("'propertyExpression' body should be a constant expression");

                object target = System.Linq.Expressions.Expression.Lambda(expression).Compile().DynamicInvoke();

                var e = new PropertyChangedEventArgs(body.Member.Name);
                handler(target, e);
            }
        }

        public static void Raise<T>(this PropertyChangedEventHandler handler, params Expression<Func<T>>[] propertyExpressions)
        {
            foreach (var propertyExpression in propertyExpressions)
            {
                handler.Raise<T>(propertyExpression);
            }
        }
    }
    
    public abstract class VmBase : ObservableModel
    {
        
    }

    public abstract class VmPage : VmBase
    {
        private string titleName;

        public string TitleName
        {
            get { return titleName; }
            set
            {
                titleName = value;
                RaisePropertyChanged(() => TitleName);
            }
        }
    }

	public abstract class VmPopupHostingPage : VmPage
	{
		private bool isPopupOpened;
		private object popupContent;
		private DataTemplate popupContentTemplate;

		public bool IsPopupOpened
		{
			get { return isPopupOpened; }
			set
			{
				isPopupOpened = value;
				InvokePropertyChanged("IsPopupOpened");
			}
		}

		public object PopupContent
		{
			get { return popupContent; }
			set
			{
				popupContent = value;
				InvokePropertyChanged("PopupContent");
			}
		}

		public DataTemplate PopupContentTemplate
		{
			get { return popupContentTemplate; }
			set
			{
				popupContentTemplate = value;
				InvokePropertyChanged("PopupContentTemplate");
			}
		}

		public void SetContentAsPopup(object content, DataTemplate contentTemplate)
		{
			PopupContent = content;
			PopupContentTemplate = contentTemplate;
			IsPopupOpened = true;
		}

		public void ClearPopup()
		{
			IsPopupOpened = true;
			IsPopupOpened = false;
		}
	}
}
