using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CardGame.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePlay : ContentPage
    {
        
        int Score;
        List<Model.CardAttributes> compete;
        double x, y;
        List<Model.CardAttributes> allAre;
        public GamePlay()
        {
            InitializeComponent();
            Score = 0;
            slider.Value = Score;
            
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    Model.AppStatus statusofUpdate = await Controller.AppVersion.CheckForUpdate();
                    if (statusofUpdate.latestversion != Model.Constants.VersionNumnber)
                    {
                        bool isupdate = await DisplayAlert("Update Available!", "Version: " + statusofUpdate.latestversion.ToString() + "Is available", "Update Now", "Cancel");
                        Device.OpenUri(new Uri(statusofUpdate.updateurl));
                    }
                }
                catch (HttpRequestException)
                {
                    DependencyService.Get<Dependencies.IToastDisplay>().SoftNotify("No Internet");
                }
                catch (Exception e)
                {
                    DependencyService.Get<Dependencies.IToastDisplay>().SoftNotify(e.Message);
                }
            });
            Controller.CardsDataController.CreateAllCards();

            allAre = new List<Model.CardAttributes>();
            allAre = Model.Constants.ListOFCards;
            BtnNext.Clicked += async (s, e) =>
              {
                  BtnNext.IsEnabled = false;
                  //PTwoCard.AnchorX = 0;
                  //POneCard.AnchorX = 0;
                  await PTwoCard.RotateTo(180, 2000);
                  await Task.WhenAll(
                   PTwoCard.RotateXTo(90, 500, Easing.SinIn),
                   POneCard.RotateXTo(-90, 500, Easing.SinIn)
                  );
                  Random random = new Random();
                  compete = new List<Model.CardAttributes>();
                  for (int n = 0; n < 2; n++)
                  {
                      int rnd = random.Next(0, allAre.Count);
                      compete.Add(allAre[rnd]);
                  }
                  PlayerOneCard.BindingContext = compete.First();
                  PlayerTwoCard.BindingContext = compete.Last();
                  if (compete.First().OverallRank > compete.Last().OverallRank)
                  {
                      LblStatus.Text = "P1 Wins";
                      Score++;
                      MyScore.Text = "Your Score is " + Score.ToString();
                      slider.Value = Score;
                  }
                  else if (compete.First().OverallRank < compete.Last().OverallRank)
                  {
                      LblStatus.Text = "P2 Wins";
                      Score--;
                      MyScore.Text = "Your Score is " + Score.ToString();
                      slider.Value = Score;
                  }
                  else
                  {
                      LblStatus.Text = "Draw";
                  }

                  if (Score == -5 || Score == 5)
                  {
                      if (Score == -5)
                      {
                          await DisplayAlert("Game Over", "You Lose!", "Retry");
                      }
                      if (Score == 5)
                      {
                          await DisplayAlert("Game Over", "You Win!", "Retry");
                      }

                      LblStatus.Text = "Status";
                      Score = 0;
                      MyScore.Text = "Your Score is " + Score.ToString();
                      slider.Value = Score;
                  }
                  await Task.WhenAll(
                      PTwoCard.RotateXTo(0, 500, Easing.SinOut),
                      POneCard.RotateXTo(0, 500, Easing.SinOut)
                  );
                  BtnNext.IsEnabled = true;
              };
        }

        public async Task<bool> OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    //PTwoCard.RotationX =
                    //  Math.Max(Math.Min(0, x - e.TotalX), -Math.Abs(PTwoCard.Width - 100));
                    //PTwoCard.RotationY = 
                    //Math.Max(Math.Min(0, y + e.TotalY), -Math.Abs(PTwoCard.Height - 0));
                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    //x = PTwoCard.RotationX;
                    await Task.WhenAll(
                   PTwoCard.RotateYTo(90, 250, Easing.BounceIn)
                  );
                    await Task.WhenAll(
            PTwoCard.RotateYTo(0, 250, Easing.BounceOut)
           );
                    //y =  PTwoCard.RotationY;
                    break;
            }
            return true;
        }

        public async Task<bool> OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            BtnNext.IsEnabled = false;
            //PTwoCard.AnchorX = 0;
            //POneCard.AnchorX = 0;
            await Task.WhenAll(
             PTwoCard.RotateXTo(90, 500, Easing.SinIn),
             POneCard.RotateXTo(-90, 500, Easing.SinIn)
            );
            Random random = new Random();
            compete = new List<Model.CardAttributes>();
            for (int n = 0; n < 2; n++)
            {
                int rnd = random.Next(0, allAre.Count);
                compete.Add(allAre[rnd]);
            }
            PlayerOneCard.BindingContext = compete.First();
            PlayerTwoCard.BindingContext = compete.Last();
            if (compete.First().OverallRank > compete.Last().OverallRank)
            {
                LblStatus.Text = "P1 Wins";
                Score++;
                MyScore.Text = "Your Score is " + Score.ToString();
                slider.Value = Score;
            }
            else if (compete.First().OverallRank < compete.Last().OverallRank)
            {
                LblStatus.Text = "P2 Wins";
                Score--;
                MyScore.Text = "Your Score is " + Score.ToString();
                slider.Value = Score;
            }
            else
            {
                LblStatus.Text = "Draw";
            }

            if (Score == -5 || Score == 5)
            {
                if (Score == -5)
                {
                    await DisplayAlert("Game Over", "You Lose!", "Retry");
                }
                if (Score == 5)
                {
                    await DisplayAlert("Game Over", "You Win!", "Retry");
                }
                LblStatus.Text = "Status";
                Score = 0;
                slider.Value = Score;
            }
            await Task.WhenAll(
                PTwoCard.RotateXTo(0, 500, Easing.SinOut),
                POneCard.RotateXTo(0, 500, Easing.SinOut)
            );
            BtnNext.IsEnabled = true;
            return true;
        }
    }
}