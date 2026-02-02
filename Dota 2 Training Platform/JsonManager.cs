using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace JsonManager
{
    public class User
    {
        public string SteamID { get; set; }
        public string Password { get; set; }
        public User(string SteamID, string Password)
        {
            this.SteamID = SteamID;
            this.Password = Password;
        }
    }
    static public class jManager
    {

        static jManager()
        {

        }
        static public void AddUser(string SteamID, string Password)
        {
            string fileName = "players.json";
            User user = new User(SteamID, Password);
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonstring = JsonSerializer.Serialize(user, options);
            File.WriteAllText(fileName, jsonstring);
        }
        public static void Savejson()
        {
        }

    }
}
