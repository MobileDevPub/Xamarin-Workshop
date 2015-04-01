using MyMovieCollection.Implementation.Services;
using MyMovieCollection.Implementation.Utilities;
using MyMovieCollection.Implementation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieCollection.Implementation
{
    /// <summary>
    /// Setup
    /// </summary>
    public static class Setup
    {
        /// <summary>
        /// Initializes all agents, managers and view models.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Niet handig hier")]
        public static void Initialize()
        {
            // Initialize agents.
            ServiceContainer.Register<ITmdbAgent>(new TmdbAgent());

            // Initialize managers

            // Initialize view models
            ServiceContainer.Register<IMyMoviesViewModel>(new MyMoviesViewModel());
        }
    }
}
