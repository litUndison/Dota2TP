using System;

namespace Dota_2_Training_Platform.Models
{
    public class TwitchStreamInfo
    {
        public string StreamId { get; set; }
        public string UserId { get; set; }
        public string UserLogin { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string GameName { get; set; }
        public int ViewerCount { get; set; }
        public DateTime StartedAtUtc { get; set; }
        public string Language { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ProfileImageUrl { get; set; }
        public string[] Tags { get; set; } = Array.Empty<string>();

        public string ChannelUrl =>
            string.IsNullOrWhiteSpace(UserLogin)
                ? "https://www.twitch.tv/directory/category/dota-2"
                : $"https://www.twitch.tv/{UserLogin}";

        public string ThumbnailUrl640 =>
            (ThumbnailUrl ?? string.Empty)
                .Replace("{width}", "640")
                .Replace("{height}", "360");

        public string ProfileImageUrl70 =>
            (ProfileImageUrl ?? string.Empty)
                .Replace("{width}", "70")
                .Replace("{height}", "70");
    }
}
