using CardGame.Helpers;
using CardGame.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CardGame.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        public GamePage()
        {
            InitializeComponent();
            BindingContext = new  GamePlayViewModel();
        }

        protected override bool OnBackButtonPressed()
        {
            MediaTypeNames.Application.Current.MainPage = new UserDetailsPage();
            return true;
        }
    }
}