using Dota_2_Training_Platform.Models;
using System;
using System.IO;
using System.Text.Json;

namespace Dota_2_Training_Platform.Functions
{
    public static class TwitchSettingsStorage
    {
        private static readonly string SettingsPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "twitch_settings.json");

        public static TwitchSettingsModel Load()
        {
            try
            {
                if (!File.Exists(SettingsPath))
                    return new TwitchSettingsModel();

                var json = File.ReadAllText(SettingsPath);
                var model = JsonSerializer.Deserialize<TwitchSettingsModel>(json);
                return Normalize(model ?? new TwitchSettingsModel());
            }
            catch
            {
                return new TwitchSettingsModel();
            }
        }

        public static void Save(TwitchSettingsModel model)
        {
            model = Normalize(model ?? new TwitchSettingsModel());
            var json = JsonSerializer.Serialize(model, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsPath, json);
        }

        private static TwitchSettingsModel Normalize(TwitchSettingsModel model)
        {
            if (model.RefreshIntervalSeconds < 5)
                model.RefreshIntervalSeconds = 5;

            if (model.StreamCount != 5 && model.StreamCount != 10 && model.StreamCount != 20)
                model.StreamCount = 5;

            return model;
        }
    }
}
