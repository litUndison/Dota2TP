using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dota_2_Training_Platform.Models
{
    public class DotaMatchModel
    {
        [JsonPropertyName("match_id")]
        public long MatchId { get; set; }

        [JsonPropertyName("player_slot")]
        public int PlayerSlot { get; set; }

        [JsonPropertyName("radiant_win")]
        public bool RadiantWin { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("game_mode")]
        public int GameMode { get; set; }

        [JsonPropertyName("lobby_type")]
        public int LobbyType { get; set; }

        [JsonPropertyName("hero_id")]
        public int HeroId { get; set; }

        [JsonPropertyName("start_time")]
        public long StartTime { get; set; }

        [JsonPropertyName("version")]
        public int? Version { get; set; }

        [JsonPropertyName("kills")]
        public int Kills { get; set; }

        [JsonPropertyName("deaths")]
        public int Deaths { get; set; }

        [JsonPropertyName("assists")]
        public int Assists { get; set; }

        [JsonPropertyName("gold_per_min")]
        public int? GoldPerMin { get; set; }

        [JsonPropertyName("last_hits")]
        public int? LastHits { get; set; }

        [JsonPropertyName("average_rank")]
        public int? AverageRank { get; set; }

        [JsonPropertyName("leaver_status")]
        public int? LeaverStatus { get; set; }

        [JsonPropertyName("party_size")]
        public int? PartySize { get; set; }

    }
}
