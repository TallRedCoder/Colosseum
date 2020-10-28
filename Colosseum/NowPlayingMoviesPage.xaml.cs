using Colosseum.Models;
using Colosseum.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Colosseum
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NowPlayingMoviesPage : ContentPage
    {
        public ObservableCollection<Movie> nowPlayingMovies;
        public NowPlayingMoviesPage()
        {
            InitializeComponent();
            nowPlayingMovies = new ObservableCollection<Movie>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (0 == nowPlayingMovies.Count)
            {
                ApiServices apiServices = new ApiServices();
                var data = await apiServices.GetNowPlayingMovies();
                foreach (var movie in data)
                    nowPlayingMovies.Add(movie);
            }

            ListNowPlaying.ItemsSource = nowPlayingMovies;
        }

        private void ListNowPlaying_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedMovie = e.SelectedItem as Movie;
            Navigation.PushAsync(new NowPlayingMovieDetailPage(selectedMovie));
        }
    }
}