using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota_2_Training_Platform.Models.Entity
{
    public class TeamEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TrainerSteamId { get; set; }
        public virtual ICollection<TeamPlayerEntity> Players { get; set; } = new List<TeamPlayerEntity>();
        public virtual ICollection<TrainingTaskEntity> Tasks { get; set; } = new List<TrainingTaskEntity>();
    }
}
