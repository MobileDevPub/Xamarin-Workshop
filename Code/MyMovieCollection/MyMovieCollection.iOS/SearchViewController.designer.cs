// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace MyMovieCollection
{
	[Register ("SearchViewController")]
	partial class SearchViewController
	{
		[Outlet]
		UIKit.UISearchBar sbSearch { get; set; }

		[Outlet]
		UIKit.UITableView tvSearchResults { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (sbSearch != null) {
				sbSearch.Dispose ();
				sbSearch = null;
			}

			if (tvSearchResults != null) {
				tvSearchResults.Dispose ();
				tvSearchResults = null;
			}
		}
	}
}
