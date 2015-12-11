using System;
using MyMovieCollection.UITest.Screens;
using MyMovieCollection.UITest.Screens.KanziScreen;
using Xamarin.UITest.Queries;

namespace MyMovieCollection.UITest
{
	public class iOSMyMoviesScreen : ScreenBase, IMyMoviesScreen
	{
		public override Func<AppQuery, AppQuery> PageTrait { get { return new Func<AppQuery, AppQuery>(c => c.Text("My Movie Collection")); } }
		
		public void Search(string movie)
		{
			App.Tap("Search");
			App.Tap(c => c.Class("UISearchBarTextField"));
			App.EnterText(movie);
			App.PressEnter();
		}
	}
}

