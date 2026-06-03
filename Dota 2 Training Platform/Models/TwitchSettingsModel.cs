namespace Dota_2_Training_Platform.Models
{
    public class TwitchSettingsModel
    {
        public bool AutoRefreshEnabled { get; set; }
        public int RefreshIntervalSeconds { get; set; } = 30;
        public int StreamCount { get; set; } = 5;

        public bool ShowTitle { get; set; } = true;
        public bool ShowAvatar { get; set; } = true;
        public bool ShowStreamer { get; set; } = true;
        public bool ShowViewers { get; set; } = true;
        public bool ShowStartedAt { get; set; } = true;
        public bool ShowLanguage { get; set; } = true;
        public bool ShowTags { get; set; } = true;
    }
}
