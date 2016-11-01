using MyMovieCollection.Implementation.Utilities;
using MyMovieCollection.Implementation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyMovieCollection
{
    public partial class MainPage : ContentPage
    {
        private IMyMoviesViewModel _viewmodel;

        public MainPage()
        {
            InitializeComponent();
            _viewmodel = ServiceContainer.Resolve<IMyMoviesViewModel>();
            listView.ItemsSource = _viewmodel.SearchResults;
        }

        private void OnValueChanged(object sender, TextChangedEventArgs e)
        {
            _viewmodel.Search(SearchEntry.Text);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //var personInfo = e.SelectedItem as Person;
            //var employeeView = new EmployeeXaml
            //{
            //    BindingContext = new PersonViewModel(personInfo, favoritesRepository)
            //};

            //Navigation.PushAsync(employeeView);
        }
    }
}
