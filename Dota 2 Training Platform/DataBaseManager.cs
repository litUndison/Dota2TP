using Dota_2_Training_Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dota_2_Training_Platform.Models;
using System.Data.SQLite;
using System.Linq;

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
            //ReadAllPlayers();
            //ReadAllTrainers();
        }

        private static void InitializeDatabase()
        {
            if (!File.Exists(dbPath))
            {
                File.Create(dbPath).Dispose();
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

                    command.CommandText =
                    @"
            CREATE TABLE IF NOT EXISTS Teams (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                TrainerSteamId TEXT NOT NULL
            );";
                    command.ExecuteNonQuery();

                    command.CommandText =
                    @"
            CREATE TABLE IF NOT EXISTS TeamPlayers (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                TeamId INTEGER NOT NULL,
                PlayerSteamId TEXT NOT NULL,
                UNIQUE(TeamId, PlayerSteamId)
            );";
                    command.ExecuteNonQuery();
                }
            }
        }

        #region PlayerMethods
        static public async Task<DotaPlayerProfileModel> AddPlayer(string steamOrAccountId, string password)
        {
            try
            {
                // Определяем, что ввёл пользователь
                long accountId;
                string steamId64;

                if (steamOrAccountId.Length > 10) // SteamID64
                {
                    steamId64 = steamOrAccountId;
                    accountId = SteamId64ToAccountId(steamId64);
                }
                else // AccountID
                {
                    accountId = long.Parse(steamOrAccountId);
                    steamId64 = AccountIdToSteamId64(accountId);
                }

                // API всегда получает AccountID
                var apiResult = await ApiCourier.TryGetUserInfo(accountId.ToString());

                if (!apiResult.IsSuccess)
                {
                    MessageBox.Show(apiResult.ErrorMessage);
                    return null;
                }

                var profileInfo = apiResult.Data;

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Проверяем по SteamID64
                    using (var checkCmd = connection.CreateCommand())
                    {
                        checkCmd.CommandText =
                            "SELECT COUNT(1) FROM Players WHERE SteamId = @steamId";

                        checkCmd.Parameters.AddWithValue("@steamId", steamId64);

                        if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                        {
                            MessageBox.Show("Пользователь с таким SteamID уже существует!");
                            return null;
                        }
                    }

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText =
                        @"
                INSERT INTO Players
                (Name, AccountId, SteamId, Password, Avatar)
                VALUES
                (@name, @accountId, @steamId, @password, @avatar);
                ";

                        command.Parameters.AddWithValue("@name",
                            profileInfo.profile.personaname.ToString() ?? "Unknown");

                        command.Parameters.AddWithValue("@accountId", accountId);
                        command.Parameters.AddWithValue("@steamId", steamId64);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@avatar",
                            profileInfo.profile.avatarfull.ToString() ?? string.Empty);

                        command.ExecuteNonQuery();

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
                var apiResult = await ApiCourier.TryGetUserInfo(SteamID);

                if (!apiResult.IsSuccess)
                {
                    MessageBox.Show(apiResult.ErrorMessage);
                    return null;
                }

                var profileInfo = apiResult.Data;

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

        #region SteamIDConverter
        private const long SteamIdOffset = 76561197960265728;

        public static long SteamId64ToAccountId(string steamId64)
        {
            return long.Parse(steamId64) - SteamIdOffset;
        }

        public static string AccountIdToSteamId64(long accountId)
        {
            return (accountId + SteamIdOffset).ToString();
        }
        #endregion


        #region ReadAllUsers

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

        #endregion



        #region GetCurrentUser

        public static UserModel GetPlayer(string steamOrAccountId, string password)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
            SELECT Name, AccountId, SteamId, Password, Avatar
            FROM Players
            WHERE (SteamId = @id OR AccountId = @id)
              AND Password = @password
            LIMIT 1;
            ";

                    command.Parameters.AddWithValue("@id", steamOrAccountId);
                    command.Parameters.AddWithValue("@password", password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserModel(
                                reader.GetString(0), // Name
                                reader.GetString(1), // AccountId
                                reader.GetString(2), // SteamId
                                reader.GetString(3), // Password
                                reader.GetString(4)  // Avatar
                            );
                        }
                    }
                }
            }

            return null;
        }
        public static UserModel GetTrainer(string steamOrAccountId, string password)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                    @"
            SELECT Name, AccountId, SteamId, Password, Avatar
            FROM Trainers
            WHERE (SteamId = @id OR AccountId = @id)
              AND Password = @password
            LIMIT 1;
            ";

                    command.Parameters.AddWithValue("@id", steamOrAccountId);
                    command.Parameters.AddWithValue("@password", password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserModel(
                                reader.GetString(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4)
                            );
                        }
                    }
                }
            }

            return null;
        }

        #endregion



        #region TeamsFunctions

        public static bool CreateTeam(string teamName, string trainerSteamId, List<string> playerSteamIds = null)
        {
            if (playerSteamIds == null)
            {
                playerSteamIds = new List<string>();
            }

            // Проверки
            if (playerSteamIds.Contains(trainerSteamId))
            {
                MessageBox.Show("Тренер не может быть участником своей команды");
                return false;
            }

            if (playerSteamIds.Count > 5)
            {
                MessageBox.Show("В команде не может быть больше 5 игроков");
                return false;
            }

            if (playerSteamIds.Distinct().Count() != playerSteamIds.Count)
            {
                MessageBox.Show("Список игроков содержит повторяющихся участников");
                return false;
            }

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        long teamId;

                        // Создаём команду
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.CommandText =
                            @"
                    INSERT INTO Teams (Name, TrainerSteamId)
                    VALUES (@name, @trainerSteamId);
                    SELECT last_insert_rowid();
                    ";

                            cmd.Parameters.AddWithValue("@name", teamName);
                            cmd.Parameters.AddWithValue("@trainerSteamId", trainerSteamId);

                            teamId = (long)cmd.ExecuteScalar();
                        }

                        // Добавляем игроков (если есть)
                        foreach (var playerSteamId in playerSteamIds)
                        {
                            using (var cmd = connection.CreateCommand())
                            {
                                cmd.CommandText =
                                @"
                        INSERT INTO TeamPlayers (TeamId, PlayerSteamId)
                        VALUES (@teamId, @playerSteamId);
                        ";

                                cmd.Parameters.AddWithValue("@teamId", teamId);
                                cmd.Parameters.AddWithValue("@playerSteamId", playerSteamId);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show(ex.Message);
                        return false;
                    }
                }
            }
        }

        public static TeamModel GetTeam(int teamId)
        {
            TeamModel team = null;

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // 1. Получаем команду
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText =
                    @"
            SELECT Id, Name, TrainerSteamId
            FROM Teams
            WHERE Id = @teamId
            ";

                    cmd.Parameters.AddWithValue("@teamId", teamId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            team = new TeamModel(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2)
                            );
                        }
                    }
                }

                if (team == null)
                    return null;

                // 2. Получаем игроков команды
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText =
                    @"
            SELECT p.Name, p.AccountId, p.SteamId, p.Password, p.Avatar
            FROM TeamPlayers tp
            JOIN Players p ON p.SteamId = tp.PlayerSteamId
            WHERE tp.TeamId = @teamId
            ";

                    cmd.Parameters.AddWithValue("@teamId", teamId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            team.Players.Add(new UserModel(
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

            return team;
        }

        public static List<TeamModel> GetTrainerTeams(string trainerSteamId)
        {
            List<TeamModel> teams = new List<TeamModel>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Получаем команды, где steamId является тренером
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText =
                    @"
            SELECT Id, Name, TrainerSteamId
            FROM Teams
            WHERE TrainerSteamId = @steamId
            ";

                    cmd.Parameters.AddWithValue("@steamId", trainerSteamId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TeamModel team = new TeamModel(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2)
                            );
                            teams.Add(team);
                        }
                    }
                }

                // Подгружаем игроков для каждой команды
                foreach (var team in teams)
                {
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText =
                        @"
                SELECT p.Name, p.AccountId, p.SteamId, p.Password, p.Avatar
                FROM TeamPlayers tp
                JOIN Players p ON p.SteamId = tp.PlayerSteamId
                WHERE tp.TeamId = @teamId
                ";
                        cmd.Parameters.Clear(); // Очистим предыдущие параметры
                        cmd.Parameters.AddWithValue("@teamId", team.Id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                team.Players.Add(new UserModel(
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
            }

            return teams;
        }

        public static List<TeamModel> GetPlayerTeams(string playerSteamId)
        {
            List<TeamModel> teams = new List<TeamModel>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Получаем команды, где steamId является игроком
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText =
                    @"
            SELECT t.Id, t.Name, t.TrainerSteamId
            FROM Teams t
            JOIN TeamPlayers tp ON t.Id = tp.TeamId
            WHERE tp.PlayerSteamId = @steamId
            ";

                    cmd.Parameters.AddWithValue("@steamId", playerSteamId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TeamModel team = new TeamModel(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2)
                            );

                            // Избегаем дублирования, если вдруг несколько записей для одного игрока
                            if (!teams.Exists(x => x.Id == team.Id))
                            {
                                teams.Add(team);
                            }
                        }
                    }
                }

                // Подгружаем игроков для каждой команды
                foreach (var team in teams)
                {
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText =
                        @"
                SELECT p.Name, p.AccountId, p.SteamId, p.Password, p.Avatar
                FROM TeamPlayers tp
                JOIN Players p ON p.SteamId = tp.PlayerSteamId
                WHERE tp.TeamId = @teamId
                ";

                        cmd.Parameters.AddWithValue("@teamId", team.Id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                team.Players.Add(new UserModel(
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
            }

            return teams;
        }


        public static bool AddPlayerToTeam(int teamId, string playerSteamId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Получаем тренера команды
                string trainerSteamId;
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT TrainerSteamId FROM Teams WHERE Id = @teamId";
                    cmd.Parameters.AddWithValue("@teamId", teamId);
                    trainerSteamId = cmd.ExecuteScalar()?.ToString();
                }

                if (trainerSteamId == playerSteamId)
                {
                    MessageBox.Show("Тренер не может быть участником команды");
                    return false;
                }

                // Проверяем количество игроков
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM TeamPlayers WHERE TeamId = @teamId";
                    cmd.Parameters.AddWithValue("@teamId", teamId);
                    if (Convert.ToInt32(cmd.ExecuteScalar()) >= 5)
                    {
                        MessageBox.Show("В команде уже 5 игроков");
                        return false;
                    }
                }

                // Проверяем, есть ли игрок уже в команде
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT COUNT(*) FROM TeamPlayers
                WHERE TeamId = @teamId AND PlayerSteamId = @playerSteamId
            ";
                    cmd.Parameters.AddWithValue("@teamId", teamId);
                    cmd.Parameters.AddWithValue("@playerSteamId", playerSteamId);

                    if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Игрок уже в этой команде");
                        return false;
                    }
                }

                // Добавляем игрока
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                INSERT INTO TeamPlayers (TeamId, PlayerSteamId)
                VALUES (@teamId, @playerSteamId)
            ";
                    cmd.Parameters.AddWithValue("@teamId", teamId);
                    cmd.Parameters.AddWithValue("@playerSteamId", playerSteamId);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
        }


        public static bool RemovePlayerFromTeam(int teamId, string playerSteamId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText =
                    @"
            DELETE FROM TeamPlayers
            WHERE TeamId = @teamId AND PlayerSteamId = @playerSteamId
            ";

                    cmd.Parameters.AddWithValue("@teamId", teamId);
                    cmd.Parameters.AddWithValue("@playerSteamId", playerSteamId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }


        #endregion



    }
}
