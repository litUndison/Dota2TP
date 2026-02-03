using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota_2_Training_Platform.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public string AccountID { get; set; }
        public string SteamID { get; set; }
        public string Password { get; set; }
        public string Avatarfull { get; set; }
        public UserModel(string Name, string AccountID, string SteamID, string Password, string Avatarfull)
        {
            this.Name = Name;
            this.AccountID = AccountID;
            this.SteamID = SteamID;
            this.Password = Password;
            this.Avatarfull = Avatarfull;
        }
    }
}
