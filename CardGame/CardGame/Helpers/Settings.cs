// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace CardGame.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string UserName = "settings_key";
        private const string IsBotOpponent = "IsBot";
        private static readonly string SettingsDefault = string.Empty;
        private static readonly bool SettingsDefaultBool = false;

        #endregion


        public static string UserNameInfo
		{
			get
			{
				return AppSettings.GetValueOrDefault(UserName, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(UserName, value);
			}
		}
        public static bool BotOpponent
        {
            get
            {
                return AppSettings.GetValueOrDefault(IsBotOpponent, SettingsDefaultBool);
            }
            set
            {
                AppSettings.AddOrUpdateValue(IsBotOpponent, value);
            }
        }

    }
}