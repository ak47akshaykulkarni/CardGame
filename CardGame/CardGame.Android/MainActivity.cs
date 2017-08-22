using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Ads;
using ImageCircle.Forms.Plugin.Droid;

namespace CardGame.Droid
{
    [Activity(Label = "GameOfCards", Icon = "@drawable/throne", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            ImageCircleRenderer.Init();
            MobileAds.Initialize(ApplicationContext, "ca-app-pub-8296970096865112~7728278184");
        }
    }
}

