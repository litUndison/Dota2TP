using Dota_2_Training_Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dota_2_Training_Platform.Models;
using System.Data.SQLite;

namespace DataBaseManager
{
    static public class dbManager
    {
        static public List<UserModel> Players { get; private set; } = new List<UserModel>();
        static public List<UserModel> Trainers { get; private set; } = new List<UserModel>();
        private static readonly string dbPath = "users.db";
        private static readonly string connectionString;
        public static void Start() { }
        static dbManager()
        {
            connectionString = $"Data Source={dbPath};Version=3;";
            InitializeDatabase();
            ReadAllPlayers();
            ReadAllTrainers();
        }

        private static void InitializeDatabase()
        {
            if (!File.Exists(dbPath))
            {
                File.Create(dbPath).Dispose(); // создаём и сразу закрываем
            }

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
                    CREATE TABLE IF NOT EXISTS Players (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT,
                        AccountId TEXT,
                        SteamId TEXT UNIQUE,
                        Password TEXT,
                        Avatar TEXT
                    );";

                    command.ExecuteNonQuery();

                    command.CommandText =
                    @"
                    CREATE TABLE IF NOT EXISTS Trainers (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT,
                        AccountId TEXT,
                        SteamId TEXT UNIQUE,
                        Password TEXT,
                        Avatar TEXT
                    );";

                    command.ExecuteNonQuery();

                    //command.CommandText =
                    //@"
                    //CREATE TABLE IF NOT EXISTS Teams (
                    //    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    //    Name TEXT,
                    //    AccountId TEXT,
                    //    SteamId TEXT UNIQUE,
                    //    Password TEXT,
                    //    Avatar TEXT
                    //);";

                    //command.ExecuteNonQuery();
                }
            }
        }
        #region PlayerMethods
        static public async Task<DotaPlayerProfileModel> AddPlayer(string SteamID, string Password)
        {
            try
            {
                var profileInfo = await ApiCourier.TryGetUserInfo(SteamID);
                if (profileInfo == null)
                    return null;

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Проверяем, есть ли пользователь с таким SteamId
                    using (var checkCmd = connection.CreateCommand())
                    {
                        checkCmd.CommandText = "SELECT COUNT(1) FROM Players WHERE SteamId = @steamId";
                        checkCmd.Parameters.AddWithValue("@steamId", profileInfo.profile.steamid);

                        var exists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;
                        if (exists)
                        {
                            MessageBox.Show("Пользователь с таким SteamID уже существует!");
                            return null; // пользователь уже есть, выходим
                        }
                    }

                    // Если пользователя нет, вставляем нового
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText =
                        @"
                INSERT INTO Players
                (Name, AccountId, SteamId, Password, Avatar)
                VALUES
                (@name, @accountId, @steamId, @password, @avatar);
                ";

                        command.Parameters.AddWithValue("@name", profileInfo.profile.personaname);
                        command.Parameters.AddWithValue("@accountId", profileInfo.profile.account_id);
                        command.Parameters.AddWithValue("@steamId", profileInfo.profile.steamid);
                        command.Parameters.AddWithValue("@password", Password);
                        command.Parameters.AddWithValue("@avatar", profileInfo.profile.avatarfull);

                        command.ExecuteNonQuery();

                        // Добавляем пользователя в локальный список
                        Players.Add(new UserModel(
                            profileInfo.profile.personaname.ToString(),
                            profileInfo.profile.account_id.ToString(),
                            profileInfo.profile.steamid.ToString(),
                            Password,
                            profileInfo.profile.avatarfull.ToString()
                        ));

                        MessageBox.Show("User saved to database");
                        return profileInfo;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        #endregion

        #region TrainerMethods
        static public async Task<DotaPlayerProfileModel> AddTrainer(string SteamID, string Password)
        {
            try
            {
                var profileInfo = await ApiCourier.TryGetUserInfo(SteamID);
                if (profileInfo == null)
                    return null;

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Проверяем, есть ли пользователь с таким SteamId
                    using (var checkCmd = connection.CreateCommand())
                    {
                        checkCmd.CommandText = "SELECT COUNT(1) FROM Trainers WHERE SteamId = @steamId";
                        checkCmd.Parameters.AddWithValue("@steamId", profileInfo.profile.steamid);

                        var exists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;
                        if (exists)
                        {
                            MessageBox.Show("Тренер с таким SteamID уже существует!");
                            return null; // пользователь уже есть, выходим
                        }
                    }

                    // Если пользователя нет, вставляем нового
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText =
                        @"
                        INSERT INTO Trainers
                        (Name, AccountId, SteamId, Password, Avatar)
                        VALUES
                        (@name, @accountId, @steamId, @password, @avatar);
                        ";

                        command.Parameters.AddWithValue("@name", profileInfo.profile.personaname);
                        command.Parameters.AddWithValue("@accountId", profileInfo.profile.account_id);
                        command.Parameters.AddWithValue("@steamId", profileInfo.profile.steamid);
                        command.Parameters.AddWithValue("@password", Password);
                        command.Parameters.AddWithValue("@avatar", profileInfo.profile.avatarfull);

                        command.ExecuteNonQuery();

                        // Добавляем пользователя в локальный список
                        Trainers.Add(new UserModel(
                            profileInfo.profile.personaname.ToString(),
                            profileInfo.profile.account_id.ToString(),
                            profileInfo.profile.steamid.ToString(),
                            Password,
                            profileInfo.profile.avatarfull.ToString()
                        ));

                        MessageBox.Show("Trainer saved to database");
                        return profileInfo;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        #endregion


        static public List<UserModel> ReadAllPlayers()
        {
            Players.Clear();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Name, AccountId, SteamId, Password, Avatar FROM Players";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Players.Add(new UserModel(
                                reader.GetString(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4)
                            ));
                        }
                    }
                }
            }

            return Players;
        }
        static public List<UserModel> ReadAllTrainers()
        {
            Trainers.Clear();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Name, AccountId, SteamId, Password, Avatar FROM Trainers";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Trainers.Add(new UserModel(
                                reader.GetString(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4)
                            ));
                        }
                    }
                }
            }

            return Trainers;
        }
    }
}
