using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota_2_Training_Platform.Models.Entity
{
    public class TeamPlayerEntity
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public virtual TeamEntity Team { get; set; }
        public string Name { get; set; }
        public string AccountId { get; set; }
        public string PlayerSteamId { get; set; }
        public string Avatar { get; set; }
    }
}
