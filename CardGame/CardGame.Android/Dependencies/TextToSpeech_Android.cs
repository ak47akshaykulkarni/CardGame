using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using CardGame.Droid.Dependencies;
using Android.Speech.Tts;
using CardGame.Dependencies;

[assembly: Dependency(typeof(TextToSpeech_Android))]
namespace CardGame.Droid.Dependencies
{
    class TextToSpeech_Android : Java.Lang.Object, ITextToSpeech , TextToSpeech.IOnInitListener
    {
        TextToSpeech speaker; string toSpeak;
        public TextToSpeech_Android() { }

        public void Speak(string text)
        {
            var c = Forms.Context;
            toSpeak = text;
            if (speaker == null)
            {
                speaker = new TextToSpeech(c, this);
            }
            else
            {
                var p = new Dictionary<string, string>();
                speaker.Speak(toSpeak, QueueMode.Flush, p);
                System.Diagnostics.Debug.WriteLine("spoke " + toSpeak);
            }
        }

        #region IOnInitListener implementation
        public void OnInit(OperationResult status)
        {
            if (status.Equals(OperationResult.Success))
            {
                System.Diagnostics.Debug.WriteLine("speaker init");
                var p = new Dictionary<string, string>();
                speaker.Speak(toSpeak, QueueMode.Flush, p);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("was quiet");
            }
        }
        #endregion
    }
}