using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace MyMovieCollection.UITest.Screens
{
    public abstract class ScreenBase : IBaseScreenInterface
    {
        protected IApp _app;

        public abstract Func<AppQuery, AppQuery> PageTrait
        {
            get;
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

        public void WaitForPage()
        {
            App.WaitForElement(PageTrait, "Page not found", new TimeSpan(0, 0, 5));
        }
    }
}