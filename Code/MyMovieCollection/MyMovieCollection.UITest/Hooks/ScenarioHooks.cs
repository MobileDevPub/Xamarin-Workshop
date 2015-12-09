using MyMovieCollection.UITest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xamarin.UITest;
using Xamarin.UITest.Configuration;

namespace MijnDienst.Droid.UITest.Steps
{
    [Binding]
    public class ScenarioHooks
    {
        private const Platform _platform = Platform.Android;

        [BeforeScenario]
        public void BeforeScenario()
        {
            AppInitializer.StartApp(_platform);
        }
    }
}
