﻿namespace DataSecurity
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Shell.Current.GoToAsync("LogInPage");
        }
    }
}
