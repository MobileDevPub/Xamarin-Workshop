using System;

/// <summary>
///  Based on the configuration of Dec. 13, 2013
/// </summary>
namespace MyMovieCollection.Model.TMDb
{
	public enum BackdropSize
	{
		w300,
		w780,
		w1280,
		original
	}

	public enum LogoSize
	{
		w45,
		w92,
		w154,
		w185,
		w300,
		w500,
		original
	}

	public enum PosterSize
	{
		w92,
		w154,
		w185,
		w342,
		w500,
		w780,
		original
	}

	public enum ProfileSize
	{
		w45,
		w185,
		h632,
		original
	}

	public enum StillSize
	{
		w92,
		w185,
		w300,
		original
	}

	public static class TmdbImage
	{
		private static string _baseUrl = "http://image.tmdb.org/t/p/";

		public static string ImageUrl(BackdropSize size, string filePath) {
			return ImageUrl ( size.ToString(), filePath );
		}

		public static string ImageUrl(LogoSize size, string filePath) {
			return ImageUrl ( size.ToString(), filePath );
		}

		public static string ImageUrl(PosterSize size, string filePath) {
			return ImageUrl ( size.ToString(), filePath );
		}

		public static string ImageUrl(ProfileSize size, string filePath) {
			return ImageUrl ( size.ToString(), filePath );
		}

		public static string ImageUrl(StillSize size, string filePath) {
			return ImageUrl ( size.ToString(), filePath );
		}

		private static string ImageUrl(string size, string filePath) {
			if(String.IsNullOrEmpty(filePath)) {
				return filePath;
			}

			return String.Format ("{0}{1}{2}", _baseUrl, size.ToString(), filePath);
		}
	}
}