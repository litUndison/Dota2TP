using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace Dota_2_Training_Platform.Models
{
    public class LobbyAndGameModes
    {
        public static Dictionary<int, string> GameModes = new Dictionary<int, string>()
        {
            { 1, "All Pick" },
            { 2, "Captain’s Mode" },
            { 22, "Ranked All Pick" },
            { 23, "Turbo" },
            { 18, "Ability Draft" }
        };

        public static Dictionary<int, string> LobbyTypes = new Dictionary<int, string>()
        {
            { 0, "Обычный" },
            { 7, "Рейтинговый" },
            { 9, "Battle Cup" }
        };
    }
    public class DotaMatchDetailsModel
    {
        public long match_id { get; set; }
        public bool radiant_win { get; set; }
        public int duration { get; set; }
        public int start_time { get; set; }
        public int lobby_type { get; set; }
        public int game_mode { get; set; }
        public int radiant_score { get; set; }
        public int dire_score { get; set; }

        public List<MatchPlayerModel> players { get; set; }
    };

    
}