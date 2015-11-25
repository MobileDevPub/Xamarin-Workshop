using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;

namespace MyMovieCollection.Android.Activities
{
    [Activity (Label = "Movie Details")]
    public class MovieDetailsActivity : AppCompatActivity
    {
        protected override void OnCreate (Bundle bundle)
        {
            // Set theme and call base implementation
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Movie);

            // Create your application here
        }
    }
}

