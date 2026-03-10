using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dota_2_Training_Platform.Models
{
    public class MatchPlayerModel
    {
        // базовые данные
        public long? account_id { get; set; }
        public int player_slot { get; set; }
        public int hero_id { get; set; }
        public string personaname { get; set; }

        public int kills { get; set; }
        public int deaths { get; set; }
        public int assists { get; set; }
        public int net_worth { get; set; }
        public bool isRadiant { get; set; }

        // дополнительные данные
        public int last_hits { get; set; }
        public int denies { get; set; }
        public int gold_per_min { get; set; }
        public int xp_per_min { get; set; }
        public int hero_damage { get; set; }
        public int tower_damage { get; set; }
        public int hero_healing { get; set; }
        public int level { get; set; }
        public double kda { get; set; }
        public PlayerBenchmarks benchmarks { get; set; }

        // предметы игрока
        public int item_0 { get; set; }
        public int item_1 { get; set; }
        public int item_2 { get; set; }
        public int item_3 { get; set; }
        public int item_4 { get; set; }
        public int item_5 { get; set; }

        public int backpack_0 { get; set; }
        public int backpack_1 { get; set; }
        public int backpack_2 { get; set; }

        public int item_neutral { get; set; }
        public int item_neutral2 { get; set; }

        // дополнительные предметы (например Aghanim's, Moon Shard)
        public int aghanims_scepter { get; set; }
        public int aghanims_shard { get; set; }
        public int moonshard { get; set; }
    }
}
