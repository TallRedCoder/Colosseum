using Colosseum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Colosseum
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NowPlayingMovieDetailPage : ContentPage
    {
        private Movie movie;
        public NowPlayingMovieDetailPage(Movie movie)
        {
            InitializeComponent();
            this.movie = movie;
            BindingContext = movie;
        }

        private void BookTicket_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BookTicketPage(movie));
        }

        private void PlayVideo_Tapped(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(movie.trailer))
                Navigation.PushAsync(new VideoPage(movie.trailer));
        }
    }
}