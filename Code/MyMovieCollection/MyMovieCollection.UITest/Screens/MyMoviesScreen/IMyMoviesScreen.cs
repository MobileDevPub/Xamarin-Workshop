using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.UITest.Queries;

namespace MyMovieCollection.UITest.Screens.KanziScreen
{
    public interface IMyMoviesScreen : IBaseScreenInterface
    {
        void Search(string movie);
    }
}
