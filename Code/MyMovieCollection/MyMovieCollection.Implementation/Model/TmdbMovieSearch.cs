using System;

/// <summary>
///  Used from WatTMDb Library
///  http://wattmdb.codeplex.com/
/// </summary>
namespace MyMovieCollection.Model.TMDb
{
	public class MovieResult : TmdbMovieBase
	{
		public bool adult { get; set; }
		public double popularity { get; set; }
	}

	public class TmdbMovieSearch : TmdbSearchResultBase<MovieResult>
	{ }
}

