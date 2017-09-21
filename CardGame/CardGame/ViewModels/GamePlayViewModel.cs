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
            IsBusy = true;
            IsBot = Settings.BotOpponent;

            PlayerFirstData = PlayerSecondData = null;
            Random random = new Random();
            int rnd = random.Next(0, allAre.Count);
            PlayerFirstData = (allAre[rnd]);
            
            rnd = random.Next(0, allAre.Count);
            playerHiddenData = (allAre[rnd]);
            CheckForWinnerCommand = new Command(CheckForWinner);
            GoToHomeCommand = new Command(GoHome);

            CompareOverallRankCommand = new Command(async () =>CompareCards(0));
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
        bool pOneWin, isBusy=true,isBot,pOnePlaying=true, pTwoPlaying= false,pTwoControls=false;
        int score = 0;
        decimal progScore=Convert.ToDecimal(0.5);

        public decimal ProgScore {
            get
            {
                int sc = Score + 5;
                progScore = Convert.ToDecimal(sc/10.0);
                return progScore;
            }
            set
            {
                progScore =  (Score+5)/10;
                OnPropertyChanged();
            }
        }

        public Command CheckForWinnerCommand{ get; }
        public Command GoToHomeCommand { get; }

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
                if(IsBot) await Task.Delay(3000);
            }
            
            int POneScore=0, PTwoScore=0;

            switch(comparer)
            {
                case 0:
                    PTwoScore = PlayerFirstData.OverallRank;
                    POneScore = PlayerSecondData.OverallRank;
                    RankBackground = "Red";
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
                POnePlaying = true;
            }
            else if (POneScore < PTwoScore)
            {
                Score--;
                POnePlaying = false;
            }
            else
            {
                Say("Death is the enemy");
                ToastDisplay("Death is the enemy!");
            }
            
            if (Score == -5 || Score == 5)
            {
                if (Score == -5)
                {
                    Say("VALAR More ghulis");
                    ToastDisplay("VALAR MORGHULI!");
                    bool GoToHome = await Application.Current.MainPage.DisplayAlert("Game Over", $"{CurrentUser} ,You Lose!", "Go to Home", "Replay");
                    if (GoToHome)
                    {
                        Application.Current.MainPage = new Views.UserDetailsPage();
                    }
                }
                if (Score == 5)
                {
                    Say("Dracarys");
                    ToastDisplay("DRACARYS!");
                    bool GoToHome = await Application.Current.MainPage.DisplayAlert("Game Over", $"{CurrentUser} ,You Win!", "Go to Home", "Replay");
                    if (GoToHome)
                    {
                        Application.Current.MainPage = new Views.UserDetailsPage();
                    }
                }
                Score = 0;
                if (IsBot) POnePlaying = true;
                return;
            }
            if(IsBot == true)
            {
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
            else
            {
                IsBusy = true;
            }
        }

        void Say(string text)
        {
            DependencyService.Get<Dependencies.ITextToSpeech>().Speak(text);
        }
        void ToastDisplay(string text)
        {
            DependencyService.Get<Dependencies.IToastDisplay>().SoftNotify(text);
        }

        async void CheckForWinner()
        {
            IsBusy = false;
            TradeCategory = "";
            PlayerFirstData = PlayerSecondData = null;
            
            Random random = new Random();
            int rnd1 = random.Next(0, allAre.Count);
            if (POnePlaying == true)
                PlayerFirstData = (allAre[rnd1]);
            else
                PlayerSecondData = (allAre[rnd1]);

            int rnd2 = random.Next(0, allAre.Count);
            if (rnd1 == rnd2)
            {
                rnd2 = random.Next(0, allAre.Count);
            }
            playerHiddenData = (allAre[rnd2]);
        }

        async void GoHome()
        {
            Application.Current.MainPage = new Views.UserDetailsPage();
        }

        public string RankBackground
        {
            get { return RankBackground; }
            set
            {
                OnPropertyChanged();
            }
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

        public bool IsBot
        {
            get { return isBot; }
            set
            {
                isBot = value;
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
                if (!isBot) PTwoControls = value;
                OnPropertyChanged();
            }
        }
        public bool PTwoControls
        {
            get { return pTwoControls; }
            set
            {
                pTwoControls = value;
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
                OnPropertyChanged(nameof(ProgScore));

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
            get { return $"Trade \n {TradeCategory}"; }
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
