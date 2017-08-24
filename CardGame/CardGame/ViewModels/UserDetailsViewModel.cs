using CardGame.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CardGame.ViewModels
{
    public class UserDetailsViewModel :INotifyPropertyChanged
    {
        public UserDetailsViewModel()
        {
            EnterUserCommand = new Command(EnterUser);
            NavigateSuggestionPage = new Command(EnterSuggestion);
            
            this.CurrentUser = Settings.UserNameInfo;
            //OnPropertyChanged(nameof(CurrentUser));
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
        public Command EnterUserCommand { get; }
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
        void EnterSuggestion()
        {
            Application.Current.MainPage = new Views.SuggestionPage();
        }
    }
    
}
