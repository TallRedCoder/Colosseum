﻿using Colosseum.Models;
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
        public NowPlayingMovieDetailPage(Movie movie)
        {
            InitializeComponent();
            BindingContext = movie;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Error", "Booking service is not working", "OK");
        }
    }
}