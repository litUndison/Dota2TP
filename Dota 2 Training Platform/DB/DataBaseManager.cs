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
        private static string connectionString;
        static dbManager()
        {
            //connectionString = $"Data Source={dbPath};Version=3;";
            //InitializeDatabase();
            //ReadAllPlayers();
            //ReadAllTrainers();
        }

        public static void InitializeDatabase()
        {
            connectionString = $"Data Source={dbPath};Version=3;";
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
            Name TEXT,
            AccountId TEXT,
            PlayerSteamId TEXT NOT NULL,
            Avatar TEXT,
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

        public static bool UpdateTeamName(int teamId, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                return false;

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Teams
                        SET Name = @name
                        WHERE Id = @teamId
                    ";

                    cmd.Parameters.AddWithValue("@name", newName);
                    cmd.Parameters.AddWithValue("@teamId", teamId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static async Task<bool> ReplacePlayerInTeam(int teamId, string oldPlayerSteamId, string newSteamOrAccountId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Проверяем, есть ли старый игрок в команде
                using (var checkCmd = connection.CreateCommand())
                {
                    checkCmd.CommandText = @"
                SELECT COUNT(*) FROM TeamPlayers
                WHERE TeamId = @teamId AND PlayerSteamId = @steamId
            ";

                    checkCmd.Parameters.AddWithValue("@teamId", teamId);
                    checkCmd.Parameters.AddWithValue("@steamId", oldPlayerSteamId);

                    if (Convert.ToInt32(checkCmd.ExecuteScalar()) == 0)
                    {
                        MessageBox.Show("Старый игрок не найден в команде");
                        return false;
                    }
                }

                // Получаем данные нового игрока через API
                var apiResult = await ApiCourier.TryGetUserInfo(newSteamOrAccountId);

                if (!apiResult.IsSuccess)
                {
                    MessageBox.Show("Новый игрок не найден в API");
                    return false;
                }

                var newPlayer = apiResult.Data.profile;
                string newSteamId = newPlayer.steamid.ToString();

                // Проверяем, не состоит ли уже в команде
                using (var checkCmd = connection.CreateCommand())
                {
                    checkCmd.CommandText = @"
                SELECT COUNT(*) FROM TeamPlayers
                WHERE TeamId = @teamId AND PlayerSteamId = @steamId
            ";

                    checkCmd.Parameters.AddWithValue("@teamId", teamId);
                    checkCmd.Parameters.AddWithValue("@steamId", newSteamId);

                    if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Новый игрок уже состоит в команде");
                        return false;
                    }
                }

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Удаляем старого игрока
                        using (var deleteCmd = connection.CreateCommand())
                        {
                            deleteCmd.CommandText = @"
                        DELETE FROM TeamPlayers
                        WHERE TeamId = @teamId AND PlayerSteamId = @steamId
                    ";

                            deleteCmd.Parameters.AddWithValue("@teamId", teamId);
                            deleteCmd.Parameters.AddWithValue("@steamId", oldPlayerSteamId);
                            deleteCmd.ExecuteNonQuery();
                        }

                        // Добавляем нового
                        using (var insertCmd = connection.CreateCommand())
                        {
                            insertCmd.CommandText = @"
                        INSERT INTO TeamPlayers
                        (TeamId, Name, AccountId, PlayerSteamId, Avatar)
                        VALUES
                        (@teamId, @name, @accountId, @steamId, @avatar)
                    ";

                            insertCmd.Parameters.AddWithValue("@teamId", teamId);
                            insertCmd.Parameters.AddWithValue("@name", newPlayer.personaname);
                            insertCmd.Parameters.AddWithValue("@accountId", newPlayer.account_id);
                            insertCmd.Parameters.AddWithValue("@steamId", newSteamId);
                            insertCmd.Parameters.AddWithValue("@avatar", newPlayer.avatarfull);

                            insertCmd.ExecuteNonQuery();
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

        public static bool CreateTeam(string teamName, string trainerSteamId, List<DotaPlayerProfileModel> players = null)
        {
            if (players == null)
                players = new List<DotaPlayerProfileModel>();

            if (players.Count > 5)
                return false;

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        long teamId;

                        // 1️⃣ Создаем команду
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = @"
                    INSERT INTO Teams (Name, TrainerSteamId)
                    VALUES (@name, @trainer);
                    SELECT last_insert_rowid();";

                            cmd.Parameters.AddWithValue("@name", teamName);
                            cmd.Parameters.AddWithValue("@trainer", trainerSteamId);

                            teamId = (long)cmd.ExecuteScalar();
                        }

                        // 2️⃣ Добавляем игроков
                        foreach (var player in players)
                        {
                            using (var cmd = connection.CreateCommand())
                            {
                                cmd.CommandText = @"
                        INSERT INTO TeamPlayers
                        (TeamId, Name, AccountId, PlayerSteamId, Avatar)
                        VALUES
                        (@teamId, @name, @accountId, @steamId, @avatar);";

                                cmd.Parameters.AddWithValue("@teamId", teamId);
                                cmd.Parameters.AddWithValue("@name", player.profile.personaname);
                                cmd.Parameters.AddWithValue("@accountId", player.profile.account_id);
                                cmd.Parameters.AddWithValue("@steamId", player.profile.steamid);
                                cmd.Parameters.AddWithValue("@avatar", player.profile.avatarfull);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
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

                // 1️⃣ Получаем команды тренера
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

                // 2️⃣ Подгружаем игроков ДЛЯ КАЖДОЙ команды
                foreach (var team in teams)
                {
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText =
                        @"
                SELECT Name, AccountId, PlayerSteamId, Avatar
                FROM TeamPlayers
                WHERE TeamId = @teamId
                ";

                        cmd.Parameters.AddWithValue("@teamId", team.Id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                team.Players.Add(new UserModel(
                                    reader.GetString(0), // Name
                                    reader.GetString(1), // AccountId
                                    reader.GetString(2), // SteamId
                                    "",                  // Password нет
                                    reader.GetString(3)  // Avatar
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

                //Получаем команды, где steamId является игроком
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

                            //Избегаем дублирования, если вдруг несколько записей для одного игрока
                            if (!teams.Exists(x => x.Id == team.Id))
                            {
                                teams.Add(team);
                            }
                        }
                    }
                }

                //Подгружаем игроков для каждой команды
                foreach (var team in teams)
                {
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText =
                        @"
                        SELECT Name, AccountId, PlayerSteamId, Avatar
                        FROM TeamPlayers
                        WHERE TeamId = @teamId
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
                                    "",
                                    reader.GetString(3)
                                ));
                            }
                        }
                    }
                }
            }

            return teams;
        }

        public static async Task<TeamModel> UpdateTeamFullAsync(
    TeamModel team,
    string newTeamName,
    string acc1,
    string acc2,
    string acc3,
    string acc4,
    string acc5)
        {
            var newAccounts = new List<string> { acc1, acc2, acc3, acc4, acc5 };

            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // ==============================
                        // 1. Обновляем имя команды
                        // ==============================
                        if (team.Name != newTeamName)
                        {
                            using (var cmd = connection.CreateCommand())
                            {
                                cmd.CommandText = "UPDATE Teams SET Name = @name WHERE Id = @id";
                                cmd.Parameters.AddWithValue("@name", newTeamName);
                                cmd.Parameters.AddWithValue("@id", team.Id);
                                await cmd.ExecuteNonQueryAsync();
                            }
                            team.Name = newTeamName; // Обновляем локально
                        }

                        // ==============================
                        // 2. Проверка дубликатов
                        // ==============================
                        var nonEmptyAccounts = newAccounts.Where(a => !string.IsNullOrEmpty(a) && a != "0").ToList();
                        if (nonEmptyAccounts.Count != nonEmptyAccounts.Distinct().Count())
                            throw new Exception("Нельзя добавить одного и того же игрока дважды");

                        // ==============================
                        // 3. Проверка на тренера
                        // ==============================
                        if (nonEmptyAccounts.Contains(team.TrainerSteamId))
                            throw new Exception("Тренер команды не может быть игроком");

                        foreach (var acc in nonEmptyAccounts)
                        {
                            using (var cmd = connection.CreateCommand())
                            {
                                cmd.CommandText = "SELECT COUNT(*) FROM Trainers WHERE SteamId = @steamId";
                                cmd.Parameters.AddWithValue("@steamId", acc);
                                if (Convert.ToInt32(await cmd.ExecuteScalarAsync()) > 0)
                                    throw new Exception("Нельзя добавить тренера как игрока");
                            }
                        }

                        // ==============================
                        // 4. Обновляем состав
                        // ==============================
                        for (int slot = 0; slot < 5; slot++)
                        {
                            string newAcc = newAccounts[slot];
                            UserModel existingPlayer = slot < team.Players.Count ? team.Players[slot] : null;

                            // Если пустой слот → удаляем
                            if (string.IsNullOrEmpty(newAcc) || newAcc == "0")
                            {
                                if (existingPlayer != null)
                                {
                                    using (var cmd = connection.CreateCommand())
                                    {
                                        cmd.CommandText = "DELETE FROM TeamPlayers WHERE TeamId = @teamId AND AccountId = @accountId";
                                        cmd.Parameters.AddWithValue("@teamId", team.Id);
                                        cmd.Parameters.AddWithValue("@accountId", existingPlayer.AccountID);
                                        await cmd.ExecuteNonQueryAsync();
                                    }
                                    team.Players[slot] = null;
                                }
                                continue;
                            }

                            // Если игрок не меняется → пропускаем
                            if (existingPlayer != null && existingPlayer.AccountID == newAcc)
                                continue;

                            // Получаем данные через API
                            var apiResult = await ApiCourier.TryGetUserInfo(newAcc);
                            if (!apiResult.IsSuccess)
                                throw new Exception($"Игрок {newAcc} не найден");

                            var p = apiResult.Data.profile;
                            var user = new UserModel(p.personaname.ToString(), p.account_id.ToString(), p.steamid.ToString(), "", p.avatarfull.ToString());

                            // Обновляем или вставляем
                            if (existingPlayer != null)
                            {
                                using (var cmd = connection.CreateCommand())
                                {
                                    cmd.CommandText = @"
                                UPDATE TeamPlayers
                                SET Name = @name,
                                    AccountId = @accountId,
                                    PlayerSteamId = @steamId,
                                    Avatar = @avatar
                                WHERE TeamId = @teamId AND AccountId = @oldAcc";

                                    cmd.Parameters.AddWithValue("@name", user.Name);
                                    cmd.Parameters.AddWithValue("@accountId", user.AccountID);
                                    cmd.Parameters.AddWithValue("@steamId", user.SteamID);
                                    cmd.Parameters.AddWithValue("@avatar", user.Avatarfull);
                                    cmd.Parameters.AddWithValue("@teamId", team.Id);
                                    cmd.Parameters.AddWithValue("@oldAcc", existingPlayer.AccountID);

                                    await cmd.ExecuteNonQueryAsync();
                                }
                                team.Players[slot] = user;
                            }
                            else
                            {
                                using (var cmd = connection.CreateCommand())
                                {
                                    cmd.CommandText = @"
                                INSERT INTO TeamPlayers
                                (TeamId, Name, AccountId, PlayerSteamId, Avatar)
                                VALUES
                                (@teamId, @name, @accountId, @steamId, @avatar)";

                                    cmd.Parameters.AddWithValue("@teamId", team.Id);
                                    cmd.Parameters.AddWithValue("@name", user.Name);
                                    cmd.Parameters.AddWithValue("@accountId", user.AccountID);
                                    cmd.Parameters.AddWithValue("@steamId", user.SteamID);
                                    cmd.Parameters.AddWithValue("@avatar", user.Avatarfull);

                                    await cmd.ExecuteNonQueryAsync();
                                }

                                if (slot < team.Players.Count)
                                    team.Players[slot] = user;
                                else
                                    team.Players.Add(user);
                            }
                        }

                        // Очистка пустых слотов в памяти
                        team.Players = team.Players.Take(5).Where(p => p != null).ToList();

                        transaction.Commit();
                        return team;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw; // Выбрасываем исключение наружу
                    }
                }
            }
        }
        public static bool AddPlayerToTeam(int teamId, string playerSteamId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                //Получаем тренера команды
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

                //Проверяем количество игроков
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

                //Проверяем, есть ли игрок уже в команде
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

                //Добавляем игрока
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
