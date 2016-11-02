using System;

/// <summary>
///  Used from WatTMDb Library
///  http://wattmdb.codeplex.com/
/// </summary>
namespace MyMovieCollection.Model.TMDb
{
	public class TmdbMovieBase
	{
		public string backdrop_path { get; set; }
		public int id { get; set; }
		public string original_title { get; set; }
		public string release_date { get; set; }
		public string poster_path { get; set; }
        public string complete_poster_path
        {
            get
            {
                return TmdbImage.ImageUrl(PosterSize.w154, poster_path); ;
            }
        }
		public string title { get; set; }
		public double vote_average { get; set; }
		public int vote_count { get; set; }

		public override string ToString()
		{
			return title;
		}
	}
}

