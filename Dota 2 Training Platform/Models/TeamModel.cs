using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dota_2_Training_Platform.Models;

namespace Dota_2_Training_Platform.Models
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Тренер
        public string TrainerSteamId { get; set; }

        // Игроки команды
        public List<UserModel> Players { get; set; }

        public TeamModel()
        {
            Players = new List<UserModel>();
        }

        public TeamModel(int id, string name, string trainerSteamId)
        {
            Id = id;
            Name = name;
            TrainerSteamId = trainerSteamId;
            Players = new List<UserModel>();
        }
    }
}

