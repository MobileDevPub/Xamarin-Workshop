// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MyMovieCollection.Implementation.ViewModels;
using MyMovieCollection.Implementation.Utilities;
using System.ComponentModel;
using System.Linq.Expressions;

namespace MyMovieCollection
{
	public partial class SearchViewController : UIViewController
	{
		private IMyMoviesViewModel _viewModel;
		
		public SearchViewController (IntPtr handle) : base (handle)
		{
			_viewModel = ServiceContainer.Resolve<IMyMoviesViewModel> ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_viewModel.PropertyChanged += ViewModelUpdate;

			sbSearch.SearchButtonClicked += (object sender, EventArgs e) => {
				((UISearchBar)sender).ResignFirstResponder(); // Hide the keyboard
				Search();
			};
		}

		public override void ViewWillUnload ()
		{
			base.ViewWillUnload ();

			_viewModel.PropertyChanged -= ViewModelUpdate;
		}

		private void ViewModelUpdate(object sender, PropertyChangedEventArgs args)
		{
			InvokeOnMainThread (() => {
				if (args.PropertyName == AsString (() => _viewModel.SearchResults)) {
					var searchResultsTableSource = new SearchResultsTableSource (_viewModel.SearchResults);
					tvSearchResults.Source = searchResultsTableSource;
					tvSearchResults.ReloadData ();

					searchResultsTableSource.MovieResultSelected += delegate(object s, int e) {
						MovieResultSelected (e);
					};
				}
			});
		}

		private async void Search() {
			await _viewModel.Search (sbSearch.Text);
		}

		private void MovieResultSelected(int selectedItem) {

			var selectedMovie = ((SearchResultsTableSource)tvSearchResults.Source).TableItems [selectedItem];
			var controller = (DetailViewController)this.Storyboard.InstantiateViewController("DetailViewController");
			controller.SelectedMovie = selectedMovie;
			this.NavigationController.PushViewController (controller, true);

		}

		/// <summary>
		/// Returns the expression member name
		/// </summary>
		/// <param name="expression">The expression.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">expression</exception>
		/// <exception cref="System.ArgumentException">Invalid expression:  + expression.Body.NodeType;expression</exception>
		private static string AsString(Expression<Func<object>> expression)
		{
			if (null == expression) throw new ArgumentNullException("expression");

			var member = expression.Body as MemberExpression;
			if (member == null)
			{
				var ubody = expression.Body as UnaryExpression;
				member = ubody.Operand as MemberExpression;
			}

			if (null == member) throw new ArgumentException("Invalid expression: " + expression.Body.NodeType, "expression");
			return member.Member.Name;

		}
	}
}