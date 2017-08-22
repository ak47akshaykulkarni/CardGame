using CardGame.Helpers;
using CardGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CardGame
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(Settings.UserNameInfo))
                App.Current.MainPage = new Views.UserDetailsPage();
            else
            {
                Constants.CurrentUser = Settings.UserNameInfo;
                MainPage = new Views.GamePage();
            }
                
            
        }

        protected override void OnStart()
        {
            //Controller.CardsDataController.CreateAllCards();
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
