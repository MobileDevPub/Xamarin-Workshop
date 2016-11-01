using MyMovieCollection.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MyMovieCollection
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Setup.Initialize();

            MainPage = new MyMovieCollection.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
