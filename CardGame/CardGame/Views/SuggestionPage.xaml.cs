using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CardGame.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuggestionPage : ContentPage
    {
        public SuggestionPage()
        {
            InitializeComponent();
            EntryDevice.Text = $"Model: {Plugin.DeviceInfo.CrossDeviceInfo.Current.Model},Version: {Plugin.DeviceInfo.CrossDeviceInfo.Current.Version}";
            EntryVersion.Text = $"App Version: {Model.Constants.VersionNumnber}";
            if(!string.IsNullOrWhiteSpace(Helpers.Settings.UserNameInfo))
                EntryName.Text = Helpers.Settings.UserNameInfo;
            
            BtnCancel.Clicked +=(s,e) =>
            {
                 Application.Current.MainPage = new UserDetailsPage();
            };
            BtnSubmit.Clicked += async (s, e) =>
            {
                if(!string.IsNullOrWhiteSpace(EntryName.Text) && !string.IsNullOrWhiteSpace(EditorSuggestion.Text))
                {
                    try
                    {
                        bool addSuggestion = await Controller.AppVersion.AddSuggestion(EntryName.Text, EditorSuggestion.Text, EntryVersion.Text, "new", EntryDevice.Text);
                        Application.Current.MainPage = new UserDetailsPage();
                    }
                    catch(Exception ee)
                    {
                        await DisplayAlert("Exception!", ee.Message, "Ok");
                    }
                }
            };
        }
    }
}