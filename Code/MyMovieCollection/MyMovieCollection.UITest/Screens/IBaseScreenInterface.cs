using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;

namespace MyMovieCollection.UITest.Screens
{
    public interface IBaseScreenInterface
    {
        Func<AppQuery, AppQuery> PageTrait { get; }

        void WaitForPage();
    }
}
