using MyMovieCollection.Model.TMDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieCollection.Implementation.Services
{

    /// <summary>
    ///  Methods for the TMDb API (The Movie Database). For more information about the available methods, check out the API.
    ///  http://docs.themoviedb.apiary.io/
    /// 
    ///  Since you would normally need an API key, we'll be routing the requests through Azure where the key is stored.
    ///  Yet, the same API calls would be redirected through Azure, which makes the API from TMDb a full mirror on Azure.
    /// </summary>
    public interface ITmdbAgent
    {
        /// <summary>
        ///  Searches through the TMDb API for a movie based on a text based.
        /// </summary>
        /// <param name="query">The query to search for</param>
        Task<TmdbMovieSearch> Search(string query);

        /// <summary>
        ///  Retrieves a specified movie based on the ID.
        /// </summary>
        /// <param name="movieId">The movie ID</param>
        Task<TmdbMovie> Movie(int movieId);
    }
}
