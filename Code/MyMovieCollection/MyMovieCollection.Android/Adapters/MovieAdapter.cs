using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyMovieCollection.Model.TMDb;
using System.Net.Http;
using Android.Graphics;
using com.refractored.monodroidtoolkit.imageloader;

namespace MyMovieCollection.Android.Adapters
{
    public class ImageLoaderWrapper : Java.Lang.Object
    {
        public TextView Name { get; set; }
        public TextView Year { get; set; }
        public ImageView Image { get; set; }
    }

    public class MovieAdapter : BaseAdapter<MovieResult>
    {
        private Context _context;
        private List<MovieResult> _items;
        private ImageLoader _imageLoader;

        public MovieAdapter (Context context, ImageLoader imageLoader, IEnumerable<MovieResult> items)
        {
            _context = context;
            _imageLoader = imageLoader;
            _items = items.ToList ();
        }

        #region implemented abstract members of BaseAdapter

        public override long GetItemId (int position)
        {
            return this [position].id;
        }

        public override View GetView (int position, View convertView, ViewGroup parent)
        {
            ImageLoaderWrapper wrapper = null;
            var view = convertView;
            if (convertView == null)
            {
                var inflaterService = (LayoutInflater)_context.GetSystemService (Context.LayoutInflaterService);
                view = inflaterService.Inflate (Resource.Layout.Movie, null, false);
                wrapper = new ImageLoaderWrapper();
                wrapper.Name = view.FindViewById<TextView>(Resource.Id.movieName);
                wrapper.Year = view.FindViewById<TextView>(Resource.Id.movieYear);
                wrapper.Image = view.FindViewById<ImageView>(Resource.Id.moviePosterImage);
                view.Tag = wrapper;
            }
            else
            {
                wrapper = convertView.Tag as ImageLoaderWrapper;
            }

            var movie = this [position];
            wrapper.Name.Text = movie.title;
            wrapper.Year.Text = string.IsNullOrWhiteSpace(movie.release_date) ? string.Empty : DateTime.Parse(movie.release_date).Year.ToString();
            wrapper.Image.SetImageResource(Resource.Drawable.Icon);
            var posterUri = TmdbImage.ImageUrl (PosterSize.w154, movie.poster_path);
            if (!string.IsNullOrWhiteSpace (posterUri)) 
            {
                _imageLoader.DisplayImage(posterUri, wrapper.Image, Resource.Drawable.Icon);
            }

            return view;
        }

        public override int Count {
            get {
                return _items.Count;
            }
        }

        public override MovieResult this [int index] {
            get {
                return _items [index];
            }
        }
        #endregion

        private async void SetPosterImageAsync(ImageView imageView, string imageUrl)
        {
            using (var client = new HttpClient ()) {
                var imageData = await client.GetByteArrayAsync (imageUrl);
                var image = await BitmapFactory.DecodeByteArrayAsync (imageData, 0, imageData.Length);
                imageView.SetImageBitmap (image);
            }
        }
    }
}

