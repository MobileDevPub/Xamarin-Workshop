using MyMovieCollection.UITest.Screens;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace MyMovieCollection.UITest.Screens.KanziScreen
{
    public class AndroidMyMoviesScreen : ScreenBase, IMyMoviesScreen
    {
        public override Func<AppQuery, AppQuery> PageTrait { get { return new Func<AppQuery, AppQuery>(c => c.Text("My Movies")); } }

        public void Search(string movie)
        {
            App.Tap(c => c.Marked("search_button"));
            App.EnterText("search_src_text", movie);
            App.DismissKeyboard();
        }
    }
}
