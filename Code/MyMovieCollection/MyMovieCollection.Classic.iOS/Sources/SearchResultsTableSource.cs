using System;
using System.Linq;
using UIKit;
using System.Collections.Generic;
using Foundation;
using SDWebImage;
using MyMovieCollection.Model.TMDb;

namespace MyMovieCollection
{
	public class SearchResultsTableSource : UITableViewSource {
	
		public event EventHandler<int> MovieResultSelected;
		public IEnumerable<MovieResult> TableItems;
		string _cellIdentifier = "TableCell";

		public SearchResultsTableSource (IEnumerable<MovieResult> items)
		{
			TableItems = items;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return TableItems.Count();
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (_cellIdentifier);

			// if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, _cellIdentifier);
			}

			var movie = TableItems.ElementAt(indexPath.Row);

			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			cell.TextLabel.Text = movie.title;

			if (!string.IsNullOrEmpty ( movie.release_date ))
            {
				cell.DetailTextLabel.Text = movie.release_date.Substring(0, 4);
			}

			var imageUrl = TmdbImage.ImageUrl (PosterSize.w154, movie.poster_path);
			if(!string.IsNullOrEmpty(imageUrl))
            {
				cell.ImageView.SetImage (
					url: new NSUrl (imageUrl),
					placeholder: UIImage.FromBundle ("movie.png")
				);
			}

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
            MovieResultSelected?.Invoke(this, indexPath.Row);
        }
	}
}

