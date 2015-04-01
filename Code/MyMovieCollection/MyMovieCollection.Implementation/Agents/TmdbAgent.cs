using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using MyMovieCollection.Model.TMDb;

namespace MyMovieCollection.Implementation.Services
{
	/// <summary>
	///  Methods for the TMDb API (The Movie Database). For more information about the available methods, check out the API.
	///  http://docs.themoviedb.apiary.io/
	/// 
	///  Since you would normally need an API key, we'll be routing the requests through Azure where the key is stored.
	///  Yet, the same API calls would be redirected through Azure, which makes the API from TMDb a full mirror on Azure.
	/// </summary>
	public class TmdbAgent : ITmdbAgent
	{
		private const string _rootUrl = "http://ccmobile.azurewebsites.net/tmdb/";

		/// <summary>
		///  Searches through the TMDb API for a movie based on a text based.
		/// </summary>
		/// <param name="query">The query to search for</param>
		public async Task<TmdbMovieSearch> Search(string query) {
			var result = await Execute (String.Format("search/movie?query={0}", query));
			var tmdbMovieSearch = JsonConvert.DeserializeObject<TmdbMovieSearch> (result);
			return tmdbMovieSearch;
		}

		/// <summary>
		///  Retrieves a specified movie based on the ID.
		/// </summary>
		/// <param name="movieId">The movie ID</param>
		public async Task<TmdbMovie> Movie(int movieId) {
			var result = await Execute (String.Format("movie/{0}", movieId));
			var tmdbMovie = JsonConvert.DeserializeObject<TmdbMovie> (result);
			return tmdbMovie;
		}

		/// <summary>
		///  Executes a direct function on the TMDb API, and will return the JSON string.
		/// </summary>
		/// <param name="query">The query to execute</param>
		private async Task<string> Execute(string query) {
			string result = null;
			
			using (var client = new HttpClient())
			{
				try
				{
					var url = String.Format("{0}{1}", _rootUrl, query);
					var response = await client.GetAsync(url);
					result = await response.Content.ReadAsStringAsync();
				}
				catch(Exception e)
				{
					return e.ToString ();
				}
			}
			return result;
		}
	}
}

