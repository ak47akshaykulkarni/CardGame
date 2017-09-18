using CardGame.ViewModels;
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
    public partial class GamePage : ContentPage
    {
        public GamePage()
        {
            InitializeComponent();
            BindingContext = new  GamePlayViewModel();

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    Model.AppStatus statusofUpdate = await Controller.AppVersion.CheckForUpdate();
                    if (statusofUpdate.latestversion != Model.Constants.VersionNumnber)
                    {
                        bool isupdate = await DisplayAlert("Update Available!", "Version: " + statusofUpdate.latestversion.ToString() + " Is available", "Update Now", "Cancel");
                        if(isupdate) Device.OpenUri(new Uri(statusofUpdate.updateurl));
                    }
                }
                catch (HttpRequestException)
                {
                    DependencyService.Get<Dependencies.IToastDisplay>().SoftNotify("No Internet to check for updates");
                }
                catch (Exception e)
                {
                    DependencyService.Get<Dependencies.IToastDisplay>().SoftNotify(e.Message);
                }
            });
        }
    }
}