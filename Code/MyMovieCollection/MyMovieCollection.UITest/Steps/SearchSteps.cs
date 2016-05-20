using System;
using TechTalk.SpecFlow;

namespace MyMovieCollection.UITest.Steps
{
    [Binding]
    public class SearchSteps : StepsBase
    {
        [Given(@"I open the app")]
        public void GivenIOpenTheApp()
        {
            MyMoviesScreen.WaitForPage();
        }
        
        [Given(@"I search for ""(.*)""")]
        public void GivenISearchFor(string movie)
        {
            MyMoviesScreen.Search(movie);
        }
        
        [Then(@"I should see ""(.*)"" in the results")]
        public void ThenIShouldSeeInTheResults(string p0)
        {
            //Check if search result is found
        }
    }
}
