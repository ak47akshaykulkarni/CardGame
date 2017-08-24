using CardGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using CardGame.Helpers;

namespace CardGame.ViewModels
{
    public class GamePlayViewModel :INotifyPropertyChanged
    {
        public GamePlayViewModel()
        {
            
            Controller.CardsDataController.CreateAllCards();
            allAre = new List<CardAttributes>();
            allAre = Constants.ListOFCards;

            PlayerFirstData = PlayerSecondData = null;
            Random random = new Random();
            int rnd = random.Next(0, allAre.Count);
            PlayerFirstData = (allAre[rnd]);
            
            rnd = random.Next(0, allAre.Count);
            playerHiddenData = (allAre[rnd]);
            CheckForWinnerCommand = new Command(CheckForWinner);

            CompareOverallRankCommand= new Command(async () =>CompareCards(0));
            CompareLoyaltyCommand         =new Command(async()=>CompareCards(1));
            CompareCunningnessCommand     =new Command(async()=>CompareCards(2));
            CompareCombatCommand = new Command(async () => CompareCards(3));

        }
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        CardAttributes playerHiddenData, playerSecondData, playerFirstData;
        
        string statusDisplay,currentUser,tradeCategory;
         List<CardAttributes> allAre;
        bool pOneWin, isBusy=true,pOnePlaying=true, pTwoPlaying= false;
        int score = 0;

        public Command CheckForWinnerCommand{ get; }

        /// <summary>
        /// Comparer
        /// </summary>
        public Command CompareOverallRankCommand { get; }
        public Command CompareLoyaltyCommand        { get; }
        public Command CompareCunningnessCommand    { get; }
        public Command CompareCombatCommand         { get; }


        async void CompareCards(int comparer)
        {
            if(IsBusy == true)
                return;

            if (POnePlaying == true)
                PlayerSecondData = playerHiddenData;
            else
            {
                PlayerFirstData = playerHiddenData;
                await Task.Delay(3000);
            }
                

            int POneScore=0, PTwoScore=0;

            switch(comparer)
            {
                case 0:
                    POneScore = PlayerFirstData.OverallRank;
                    PTwoScore = PlayerSecondData.OverallRank;
                    TradeCategory = "Overall Rank";
                    break;
                case 1:
                    POneScore = PlayerFirstData.LoyaltyScore;
                    PTwoScore = PlayerSecondData.LoyaltyScore;
                    TradeCategory = "Loyalty";
                    break;
                case 2:
                    POneScore = PlayerFirstData.CunningnessScore;
                    PTwoScore = PlayerSecondData.CunningnessScore;
                    TradeCategory = "Cunningness";
                    break;
                case 3:
                    POneScore = PlayerFirstData.CombatScore;
                    PTwoScore = PlayerSecondData.CombatScore;
                    TradeCategory = "Combat";
                    break;
                default:
                    POneScore = PlayerFirstData.OverallRank;
                    PTwoScore = PlayerSecondData.OverallRank;
                    TradeCategory = "Overall Rank";
                        break;
            }

            if (POneScore > PTwoScore)
            {
                Score++;
                pOneWin = true;
                POnePlaying = true;
            }
            else if (POneScore < PTwoScore)
            {
                Score--;
                pOneWin = false;
                POnePlaying = false;
            }
            else
            {
                DependencyService.Get<Dependencies.ITextToSpeech>().Speak("Death is the enemy");
                DependencyService.Get<Dependencies.IToastDisplay>().SoftNotify("Death is the enemy!");
            }
            
            if (Score == -5 || Score == 5)
            {
                if (Score == -5)
                {
                    DependencyService.Get<Dependencies.ITextToSpeech>().Speak("VALAR More ghulis");
                    DependencyService.Get<Dependencies.IToastDisplay>().SoftNotify("VALAR MORGHULI!");
                    bool isRetry = await Application.Current.MainPage.DisplayAlert("Game Over", $"{CurrentUser} ,You Lose!", "Go to Home", "Retry");
                    if (isRetry)
                    {
                        Application.Current.MainPage = new Views.UserDetailsPage();
                    }
                }
                if (Score == 5)
                {
                    DependencyService.Get<Dependencies.ITextToSpeech>().Speak("Dracarys");
                    DependencyService.Get<Dependencies.IToastDisplay>().SoftNotify("DRACARYS!");
                    bool isRetry = await Application.Current.MainPage.DisplayAlert("Game Over", $"{CurrentUser} ,You Win!", "Go to Home", "Retry");
                    if (isRetry)
                    {
                        Application.Current.MainPage = new Views.UserDetailsPage();
                    }
                }
                Score = 0;
            }
            if (POnePlaying == false)
            {
                
                CheckForWinner();
                await Task.Delay(2000);
                Random r = new Random();
                CompareCards(r.Next(0, 3));
            }
            else
            {
                IsBusy = true;
            }
        }

        async void CheckForWinner()
        {
            IsBusy = false;
            TradeCategory = "";
            PlayerFirstData = PlayerSecondData = null;
            
            Random random = new Random();
            int rnd = random.Next(0, allAre.Count);
            if (POnePlaying == true)
                PlayerFirstData = (allAre[rnd]);
            else
                PlayerSecondData = (allAre[rnd]);

            rnd = random.Next(0, allAre.Count);
            playerHiddenData = (allAre[rnd]);
        }

        public string CurrentUser { get { return Constants.CurrentUser; } }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }
        public bool POnePlaying
        {
            get { return pOnePlaying; }
            set
            {
                pOnePlaying = value;
                PTwoPlaying = !value;
                OnPropertyChanged();
            }
        }
        public bool PTwoPlaying
        {
            get { return pTwoPlaying; }
            set
            {
                pTwoPlaying = value;
                OnPropertyChanged();
            }
        }
        public int Score
        {
            get {return score; }
            set
            {
                score = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(StatusDisplay));
            }
        }

        public string TradeCategory
        {
            get { return tradeCategory; }
            set
            {
                tradeCategory = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(StatusDisplay));
            }
        }

        public string StatusDisplay
        {
            get { return $"{(pOneWin ? "P1Wins":"Wait P2Wins")} Trade: {TradeCategory}"; }
        }

        public CardAttributes PlayerFirstData
        {
            get { return playerFirstData; }
            set
            {
                playerFirstData = value;
                OnPropertyChanged();
            }
        }
        public CardAttributes PlayerSecondData
        {
            get { return playerSecondData; }
            set
            {
                playerSecondData = value;
                OnPropertyChanged();
            }
        }
    }
}
