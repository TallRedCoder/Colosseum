using Colosseum.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Colosseum
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookTicketPage : ContentPage
    {
        public BookTicketPage(Movie movie)
        {
            InitializeComponent();
            BindingContext = movie;
        }
    }
}