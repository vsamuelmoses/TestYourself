﻿using System;
using System.Reflection;
using System.Windows;
using Microsoft.Phone.Controls;
using TestYourself.Helpers;

namespace TestYourself.View
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            textBlockVersion.Text ="Version " + AppSettings.GetVersionNumber();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/SubjectPanoramaPage.xaml", UriKind.Relative));
        }
    }
}