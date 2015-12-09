using MyMovieCollection.UITest.Screens;
using MyMovieCollection.UITest.Screens.KanziScreen;
using System;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace MyMovieCollection.UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                var app = ConfigureApp.Android.StartApp();
                FeatureContext.Current.Add(ScreenNames.MyMovies, new AndroidMyMoviesScreen());
                FeatureContext.Current.Add("App", app);
                return app;
            }
            else
            {
                var app = ConfigureApp.iOS.StartApp();
                //FeatureContext.Current.Add(ScreenNames.MyMovies, new iOSMyMoviesScreen());
                FeatureContext.Current.Add("App", app);
                return app;
            }
        }
    }
}

