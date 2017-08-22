using Android.Widget;
using Xamarin.Forms;
using CardGame.Droid.Dependencies;
using CardGame.Dependencies;

[assembly: Dependency(typeof(DisplayAlert))]
namespace CardGame.Droid.Dependencies
{
    public class DisplayAlert : IToastDisplay
    {
        public void SoftNotify(string Notify)
        {
            var c = Forms.Context;
            Toast.MakeText(c, Notify, ToastLength.Long).Show();
        }
    }
}