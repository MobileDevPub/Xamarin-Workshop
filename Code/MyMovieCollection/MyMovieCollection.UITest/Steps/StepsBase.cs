using MyMovieCollection.UITest.Screens;
using MyMovieCollection.UITest.Screens.KanziScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xamarin.UITest;

namespace MyMovieCollection.UITest.Steps
{
    public abstract class StepsBase
    {
        protected IMyMoviesScreen _myMoviesScreen;
        protected IApp _app;

        protected IMyMoviesScreen MyMoviesScreen
        {
            get
            {
                if (_myMoviesScreen == null)
                {
                    _myMoviesScreen = FeatureContext.Current.Get<IMyMoviesScreen>(ScreenNames.MyMovies);
                }
                return _myMoviesScreen;
            }
        }

        protected IApp App
        {
            get
            {
                if (_app == null)
                {
                    _app = FeatureContext.Current.Get<IApp>("App");
                }
                return _app;
            }
        }
    }
}
