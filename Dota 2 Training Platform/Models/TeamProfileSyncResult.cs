using System.Collections.Generic;

namespace Dota_2_Training_Platform.Models
{
    public class TeamProfileSyncResult
    {
        public TeamModel Team { get; set; }
        public UserModel Trainer { get; set; }
        public List<string> ChangedPlayers { get; set; } = new List<string>();
        public bool TrainerUpdated { get; set; }

        public bool HasChanges => ChangedPlayers.Count > 0 || TrainerUpdated;
    }
}
