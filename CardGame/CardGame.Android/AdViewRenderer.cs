
using Android.Widget;
using Android.Gms.Ads;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CardGame.Droid;
using CardGame;
[assembly: ExportRenderer(typeof(CardGame.CustomControl.AdControlView), typeof(CardGame.Droid.AdViewRenderer))]
namespace CardGame.Droid
{
    public class AdViewRenderer : ViewRenderer<CardGame.CustomControl.AdControlView, AdView>
    {
        string adUnitId = string.Empty;
        //Note you may want to adjust this, see further down.
        AdSize adSize = AdSize.SmartBanner;
        AdView adView;
        AdView CreateNativeAdControl()
        {
            if (adView != null)
                return adView;

            // This is a string in the Resources/values/strings.xml that I added or you can modify it here. This comes from admob and contains a / in it
            adUnitId = "ca-app-pub-8296970096865112/9205011387";
            adView = new AdView(Forms.Context);
            adView.AdSize = adSize;
            adView.AdUnitId = adUnitId;

            var adParams = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);

            adView.LayoutParameters = adParams;

            adView.LoadAd(new AdRequest
                            .Builder()
                            .Build());
            return adView;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CardGame.CustomControl.AdControlView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                CreateNativeAdControl();
                SetNativeControl(adView);
            }
        }
    }
}