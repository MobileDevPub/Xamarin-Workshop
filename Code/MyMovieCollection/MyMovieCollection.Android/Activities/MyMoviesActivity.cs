using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using MyMovieCollection.Implementation.Services;
using MyMovieCollection.Android.Adapters;
using MyMovieCollection.Implementation.ViewModels;
using System.Linq.Expressions;
using com.refractored.monodroidtoolkit.imageloader;
using MyMovieCollection.Implementation.Utilities;

namespace MyMovieCollection.Android.Activities
{
    [Activity (Label = "My Movies", MainLauncher = true)]
    public class MyMoviesActivity : ActionBarActivity, SearchView.IOnQueryTextListener
    {
        private ListView _list;
        private IMyMoviesViewModel _viewmodel;
        private ImageLoader _imageLoader;

        /// <summary>
        /// ctor
        /// </summary>
        public MyMoviesActivity()
        {
            _viewmodel = ServiceContainer.Resolve<IMyMoviesViewModel>();
        }

        protected override void OnCreate (Bundle bundle)
        {
            // Set theme and call base implementation
            base.OnCreate (bundle);

            _imageLoader = new ImageLoader(this, 200);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.MyMovies);
            _list = FindViewById<ListView>(Resource.Id.list);
            _list.ItemClick += _list_ItemClick;
            _list.EmptyView = FindViewById<TextView>(Resource.Id.empty);
        }

        /// <summary>
        /// Called when the activity is started
        /// </summary>
        protected override void OnStart()
        {
            base.OnStart();
            _viewmodel.PropertyChanged += ViewModelUpdate;
        }

        /// <summary>
        /// Called when the activity is stopped
        /// </summary>
        protected override void OnStop()
        {
            base.OnStop();
            _viewmodel.PropertyChanged -= ViewModelUpdate;
        }

        private void ViewModelUpdate(object sender, System.ComponentModel.PropertyChangedEventArgs args)
        {
            RunOnUiThread(() =>
            {
                if(args.PropertyName == AsString(() => _viewmodel.SearchResults))
                {
                    _list.Adapter = new MovieAdapter(this, _imageLoader, _viewmodel.SearchResults);
                }
            });
        }

        void _list_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            StartActivity(typeof(MovieDetailsActivity));
        }

        public override bool OnCreateOptionsMenu (IMenu menu)
        {
            //Create the search view
            SearchView searchView = new SearchView (SupportActionBar.ThemedContext);
            searchView.SetQueryHint ("Search for movies...");
            searchView.SetOnQueryTextListener (this);

            // Add it to the ActionBar
            menu.Add ("Search")
                .SetIcon (global::Android.Resource.Drawable.IcMenuSearch)
                .SetActionView (searchView)
                .SetShowAsAction (ShowAsAction.Always);

            return true;
        }

        public bool OnQueryTextChange (string newText)
        {
            Search(newText);
            return false;
        }

        public bool OnQueryTextSubmit (string query)
        {
            return false;
        }

        private async void Search(string query)
        {
            await _viewmodel.Search(query);
        }

        /// <summary>
        /// Returns the expression member name
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">expression</exception>
        /// <exception cref="System.ArgumentException">Invalid expression:  + expression.Body.NodeType;expression</exception>
        private static string AsString(Expression<Func<object>> expression)
        {
            if (null == expression) throw new ArgumentNullException("expression");

            var member = expression.Body as MemberExpression;
            if (member == null)
            {
                var ubody = expression.Body as UnaryExpression;
                member = ubody.Operand as MemberExpression;
            }

            if (null == member) throw new ArgumentException("Invalid expression: " + expression.Body.NodeType, "expression");
            return member.Member.Name;

        }
    }
}


