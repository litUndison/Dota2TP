using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota_2_Training_Platform.Models.Entity
{
    public class PlayerEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string AccountId { get; set; }
        public string SteamId { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
    }
}
