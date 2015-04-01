using System;
using System.Collections.Generic;

/// <summary>
///  Used from WatTMDb Library
///  http://wattmdb.codeplex.com/
/// </summary>
namespace MyMovieCollection.Model.TMDb
{
	public abstract class TmdbSearchResultBase<T>
	{
		public int page { get; set; }
		public List<T> results { get; set; }
		public int total_pages { get; set; }
		public int total_results { get; set; }
	}
}

