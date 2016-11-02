using MyMovieCollection.Implementation.Utilities;
using MyMovieCollection.Implementation.ViewModels;
using MyMovieCollection.Model.TMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyMovieCollection
{
    public partial class MainPage : ContentPage
    {
        private IMyMoviesViewModel _viewmodel;

        public MainPage()
        {
            InitializeComponent();
            Title = "Movies";

            _viewmodel = ServiceContainer.Resolve<IMyMoviesViewModel>();
            listView.ItemsSource = _viewmodel.SearchResults;
        }

        private void OnValueChanged(object sender, TextChangedEventArgs e)
        {
            Task.Run(() => _viewmodel.Search(SearchEntry.Text));
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var movie = e.SelectedItem as MovieResult;

            var movieDetailPage = new MovieDetailPage();
            Navigation.PushAsync(movieDetailPage);
        }
    }
}
