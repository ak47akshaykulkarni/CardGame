using Plugin.Share;
using CardGame.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Share.Abstractions;

namespace CardGame.ViewModels
{
    public class UserDetailsViewModel :INotifyPropertyChanged
    {
        public UserDetailsViewModel()
        {
            
            EnterOnePlayerCommand = new Command(EnterOnePlayer);
            EnterTwoPlayerCommand = new Command(EnterTwoPlayer);
            NavigateSuggestionPage = new Command(EnterSuggestion);
            ShareCommand = new Command(ShareApp);
            RateCommand = new Command(RateApp);


            this.CurrentUser = Settings.UserNameInfo;
            VersionNumber = Model.Constants.VersionNumnber;
        }

        

        string currentUser, isCurrentUser;
        bool isEnterText;
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public int VersionNumber { get; set; }
        
        public Command EnterOnePlayerCommand { get; }
        public Command EnterTwoPlayerCommand { get; }
        public Command ShareCommand { get; }
        public Command RateCommand { get; }
        public Command NavigateSuggestionPage { get; }

        public bool IsEnterText
        {
            get { return isEnterText; }
            set
            {
                isEnterText = string.IsNullOrWhiteSpace(CurrentUser) ? false : true;
                OnPropertyChanged();
            }
        }
        public string Version
        {
            get
            {
                return $"Version {VersionNumber}";
            }
        }

        public string CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                IsEnterText = string.IsNullOrWhiteSpace(CurrentUser) ? false : true;
                OnPropertyChanged();
            }
            
        }
         void EnterUser()
        {
            Settings.UserNameInfo = CurrentUser;
            Model.Constants.CurrentUser= CurrentUser;
            Application.Current.MainPage=new Views.GamePage();
        }
        void EnterOnePlayer()
        {
            Settings.UserNameInfo = CurrentUser;
            Settings.BotOpponent = true;
            Model.Constants.CurrentUser = CurrentUser;
            Application.Current.MainPage = new Views.GamePage();
        }
        void EnterTwoPlayer()
        {
            Settings.UserNameInfo = CurrentUser;
            Settings.BotOpponent = false;
            Model.Constants.CurrentUser = CurrentUser;
            Application.Current.MainPage = new Views.GamePage();
        }
        void EnterSuggestion()
        {
            Application.Current.MainPage = new Views.SuggestionPage();
        }
        private async void ShareApp(object obj)
        {
            ShareMessage newmsg = new ShareMessage()
            {
                Text = "Hey GoT fan, Checkout this awesome GoT trumpCards App I've been playing ",
                Title = "Download Game of Cards",
                Url = "https://play.google.com/store/apps/details?id=com.yekarlo.gameofcards"
            };
            await CrossShare.Current.Share(newmsg);
        }
        private async void RateApp(object obj)
        {
            await CrossShare.Current.OpenBrowser("https://play.google.com/store/apps/details?id=com.yekarlo.gameofcards");
        }
    }
    
}
