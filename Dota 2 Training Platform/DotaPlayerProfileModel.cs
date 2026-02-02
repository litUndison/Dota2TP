using System;
using System.ComponentModel;
using System.Security;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dota_2_Training_Platform
{
    public class DotaPlayerProfileModel
    {
        public DotaPlayerProfileInfo profile { get; set; } = new DotaPlayerProfileInfo();

    }

    public class DotaPlayerProfileInfo
    {
        
        public JsonElement account_id { get; set; }

        public JsonElement personaname { get; set; }

        public JsonElement plus { get; set; }

        public JsonElement steamid { get; set; }

        public JsonElement avatar { get; set; }

        public JsonElement avatarmedium { get; set; }

        public JsonElement avatarfull { get; set; }

        public JsonElement profileurl { get; set; }



        public override string ToString()
        {
            return $"account_id: {account_id.ToString()}, \n" +
                $"personaname: {personaname.ToString()}";
        }
    }
}
