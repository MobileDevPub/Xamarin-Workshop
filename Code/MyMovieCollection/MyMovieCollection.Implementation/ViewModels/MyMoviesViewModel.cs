using MyMovieCollection.Implementation.Services;
using MyMovieCollection.Implementation.Utilities;
using MyMovieCollection.Model.TMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieCollection.Implementation.ViewModels
{
    public class MyMoviesViewModel : ViewModelBase, IMyMoviesViewModel
    {
        private ITmdbAgent _tmdbAgent;

        private List<MovieResult> _searchResults;

        public List<MovieResult> SearchResults
        {
            get 
            {
                if(_searchResults == null)
                {
                    _searchResults = new List<MovieResult>();
                }
                return _searchResults;
            }
            private set
            {
                if (_searchResults != value)
                {
                    _searchResults = value;
                    OnPropertyChanged();
                }
            }
        }

        public MyMoviesViewModel() : this (ServiceContainer.Resolve<ITmdbAgent>())
        { }

        public MyMoviesViewModel(ITmdbAgent tmdbAgent)
        {
            _tmdbAgent = tmdbAgent;
        }

        public async Task Search(string query)
        {
            var result = await _tmdbAgent.Search(query);
            SearchResults = result.results;
        }
    }
}
