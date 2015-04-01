using MyMovieCollection.Model.TMDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieCollection.Implementation.ViewModels
{
    public interface IMyMoviesViewModel : INotifyPropertyChanged
    {
        List<MovieResult> SearchResults { get; }

        Task Search(string query);
    }
}
