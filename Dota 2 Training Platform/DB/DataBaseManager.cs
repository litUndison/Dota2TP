using Dota_2_Training_Platform;
using Dota_2_Training_Platform.DB;
using Dota_2_Training_Platform.Models;
using Dota_2_Training_Platform.Models.Entity;
using Dota_2_Training_Platform.Models.Trainings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseManager
{
    public static class dbManager
    {
        public static List<UserModel> Players { get; private set; } = new List<UserModel>();
        public static List<UserModel> Trainers { get; private set; } = new List<UserModel>();

        private static AppDbContext CreateContext() => new AppDbContext();

        public static async void InitializeDatabase()
        {
            using (var context = new AppDbContext())
            {
                await context.Database.MigrateAsync();
            }
        }

        #region PlayerMethods

        public static async Task<DotaPlayerProfileModel> AddPlayer(string login, string steamOrAccountId, string password)
        {
            try
            {
                login = NormalizeLogin(login);
                if (string.IsNullOrWhiteSpace(login))
                {
                    MessageBox.Show("Введите логин.");
                    return null;
                }

                if (login.Length < 3)
                {
                    MessageBox.Show("Логин должен содержать минимум 3 символа.");
                    return null;
                }

                long accountId;
                string steamId64;

                if (steamOrAccountId.Length > 10)
                {
                    steamId64 = steamOrAccountId;
                    accountId = SteamId64ToAccountId(steamId64);
                }
                else
                {
                    accountId = long.Parse(steamOrAccountId);
                    steamId64 = AccountIdToSteamId64(accountId);
                }

                var apiResult = await ApiCourier.TryGetUserInfo(accountId.ToString());
                if (!apiResult.IsSuccess)
                {
                    MessageBox.Show(apiResult.ErrorMessage);
                    return null;
                }

                var profileInfo = apiResult.Data;

                using (var context = CreateContext())
                {
                    if (await context.Players.AnyAsync(p => p.Login == login))
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует!");
                        return null;
                    }

                    if (await context.Trainers.AnyAsync(t => t.Login == login))
                    {
                        MessageBox.Show("Этот логин уже занят тренером.");
                        return null;
                    }

                    if (await context.Players.AnyAsync(p => p.SteamId == steamId64))
                    {
                        MessageBox.Show("Пользователь с таким SteamID уже существует!");
                        return null;
                    }

                    context.Players.Add(new PlayerEntity
                    {
                        Login = login,
                        Name = profileInfo.profile.personaname.ToString() ?? "Unknown",
                        AccountId = accountId.ToString(),
                        SteamId = steamId64,
                        Password = password,
                        Avatar = profileInfo.profile.avatarfull.ToString() ?? string.Empty
                    });

                    await context.SaveChangesAsync();
                    return profileInfo;
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

        public static async Task<DotaPlayerProfileModel> AddTrainer(string login, string steamOrAccountId, string password)
        {
            try
            {
                login = NormalizeLogin(login);
                if (string.IsNullOrWhiteSpace(login))
                {
                    MessageBox.Show("Введите логин.");
                    return null;
                }

                if (login.Length < 3)
                {
                    MessageBox.Show("Логин должен содержать минимум 3 символа.");
                    return null;
                }

                var apiResult = await ApiCourier.TryGetUserInfo(steamOrAccountId);
                if (!apiResult.IsSuccess)
                {
                    MessageBox.Show(apiResult.ErrorMessage);
                    return null;
                }

                var profileInfo = apiResult.Data;
                var profile = profileInfo.profile;

                using (var context = CreateContext())
                {
                    if (await context.Trainers.AnyAsync(t => t.Login == login))
                    {
                        MessageBox.Show("Тренер с таким логином уже существует!");
                        return null;
                    }
                    if (await context.Players.AnyAsync(p => p.Login == login))
                    {
                        MessageBox.Show("Этот логин уже занят игроком.");
                        return null;
                    }

                    if (await context.Trainers.AnyAsync(t => t.SteamId == profile.steamid.ToString()))
                    {
                        MessageBox.Show("Тренер с таким SteamID уже существует!");
                        return null;
                    }

                    context.Trainers.Add(new TrainerEntity
                    {
                        Login = login,
                        Name = profile.personaname.ToString(),
                        AccountId = profile.account_id.ToString(),
                        SteamId = profile.steamid.ToString(),
                        Password = password,
                        Avatar = profile.avatarfull.ToString()
                    });

                    await context.SaveChangesAsync();

                    Trainers.Add(new UserModel(
                        profile.personaname.ToString(),
                        profile.account_id.ToString(),
                        profile.steamid.ToString(),
                        password,
                        profile.avatarfull.ToString()));

                    //MessageBox.Show("Trainer saved to database");
                    return profileInfo;
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

        public static List<UserModel> ReadAllPlayers()
        {
            Players.Clear();

            using (var context = CreateContext())
            {
                Players.AddRange(
                    context.Players
                        .AsNoTracking()
                        .Select(ToUserModel)
                        .ToList());
            }

            return Players;
        }

        public static List<UserModel> ReadAllTrainers()
        {
            Trainers.Clear();

            using (var context = CreateContext())
            {
                Trainers.AddRange(
                    context.Trainers
                        .AsNoTracking()
                        .Select(ToUserModel)
                        .ToList());
            }

            return Trainers;
        }

        #endregion

        #region GetCurrentUser

        public static UserModel GetPlayer(string loginOrLegacyId, string password)
        {
            string normalizedLogin = NormalizeLogin(loginOrLegacyId);
            using (var context = CreateContext())
            {
                var entity = context.Players
                    .AsNoTracking()
                    .FirstOrDefault(p =>
                        p.Password == password &&
                        (p.Login == normalizedLogin ||
                         p.SteamId == loginOrLegacyId ||
                         p.AccountId == loginOrLegacyId));

                return entity == null ? null : ToUserModel(entity);
            }
        }

        public static UserModel GetTrainer(string loginOrLegacyId, string password)
        {
            string normalizedLogin = NormalizeLogin(loginOrLegacyId);
            using (var context = CreateContext())
            {
                var entity = context.Trainers
                    .AsNoTracking()
                    .FirstOrDefault(t =>
                        t.Password == password &&
                        (t.Login == normalizedLogin ||
                         t.SteamId == loginOrLegacyId ||
                         t.AccountId == loginOrLegacyId));

                return entity == null ? null : ToUserModel(entity);
            }
        }

        private static string NormalizeLogin(string login)
        {
            return string.IsNullOrWhiteSpace(login)
                ? string.Empty
                : login.Trim().ToLowerInvariant();
        }

        #endregion

        #region TeamsFunctions

        public static bool UpdateTeamName(int teamId, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                return false;

            using (var context = CreateContext())
            {
                var team = context.Teams.Find(teamId);
                if (team == null)
                    return false;

                team.Name = newName;
                return context.SaveChanges() > 0;
            }
        }

        public static async Task<bool> ReplacePlayerInTeam(int teamId, string oldPlayerSteamId, string newSteamOrAccountId)
        {
            using (var context = CreateContext())
            {
                var oldPlayer = await context.TeamPlayers
                    .FirstOrDefaultAsync(tp =>
                        tp.TeamId == teamId && tp.PlayerSteamId == oldPlayerSteamId);

                if (oldPlayer == null)
                {
                    MessageBox.Show("Старый игрок не найден в команде");
                    return false;
                }

                var apiResult = await ApiCourier.TryGetUserInfo(newSteamOrAccountId);
                if (!apiResult.IsSuccess)
                {
                    MessageBox.Show("Новый игрок не найден в API");
                    return false;
                }

                var newPlayer = apiResult.Data.profile;
                string newSteamId = newPlayer.steamid.ToString();

                if (await context.TeamPlayers.AnyAsync(tp =>
                        tp.TeamId == teamId && tp.PlayerSteamId == newSteamId))
                {
                    MessageBox.Show("Новый игрок уже состоит в команде");
                    return false;
                }

                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        context.TeamPlayers.Remove(oldPlayer);
                        context.TeamPlayers.Add(new TeamPlayerEntity
                        {
                            TeamId = teamId,
                            Name = newPlayer.personaname.ToString(),
                            AccountId = newPlayer.account_id.ToString(),
                            PlayerSteamId = newSteamId,
                            Avatar = newPlayer.avatarfull.ToString()
                        });

                        await context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
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

            using (var context = CreateContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var team = new TeamEntity
                    {
                        Name = teamName,
                        TrainerSteamId = trainerSteamId
                    };

                    foreach (var player in players)
                    {
                        team.Players.Add(new TeamPlayerEntity
                        {
                            Name = player.profile.personaname.ToString(),
                            AccountId = player.profile.account_id.ToString(),
                            PlayerSteamId = player.profile.steamid.ToString(),
                            Avatar = player.profile.avatarfull.ToString()
                        });
                    }

                    context.Teams.Add(team);
                    context.SaveChanges();
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

        public static TeamModel GetTeam(int teamId)
        {
            using (var context = CreateContext())
            {
                var team = context.Teams
                    .AsNoTracking()
                    .Include(t => t.Players)
                    .FirstOrDefault(t => t.Id == teamId);

                if (team == null)
                    return null;

                var result = new TeamModel(team.Id, team.Name, team.TrainerSteamId);

                foreach (var tp in team.Players)
                {
                    var player = context.Players
                        .AsNoTracking()
                        .FirstOrDefault(p => p.SteamId == tp.PlayerSteamId);

                    if (player != null)
                        result.Players.Add(ToUserModel(player));
                    else
                        result.Players.Add(ToUserModel(tp));
                }

                return result;
            }
        }

        public static List<TeamModel> GetTrainerTeams(string trainerSteamId)
        {
            using (var context = CreateContext())
            {
                var teams = context.Teams
                    .AsNoTracking()
                    .Include(t => t.Players)
                    .Where(t => t.TrainerSteamId == trainerSteamId)
                    .ToList();

                return teams.Select(MapTeam).ToList();
            }
        }

        public static List<TeamModel> GetPlayerTeams(string playerSteamId)
        {
            using (var context = CreateContext())
            {
                var teams = context.Teams
                    .AsNoTracking()
                    .Include(t => t.Players)
                    .Where(t => t.Players.Any(p => p.PlayerSteamId == playerSteamId))
                    .ToList();

                return teams
                    .GroupBy(t => t.Id)
                    .Select(g => MapTeam(g.First()))
                    .ToList();
            }
        }

        /// <summary>
        /// Запрашивает актуальные ник и аватар у API для состава и тренера команды.
        /// При отличии от БД обновляет TeamPlayers, Trainers и при наличии — Players.
        /// </summary>
        public static async Task<TeamProfileSyncResult> SyncTeamProfilesFromApiAsync(int teamId)
        {
            using (var context = CreateContext())
            {
                var teamEntity = await context.Teams
                    .Include(t => t.Players)
                    .FirstOrDefaultAsync(t => t.Id == teamId);

                if (teamEntity == null)
                    return null;

                var result = new TeamProfileSyncResult();

                foreach (var teamPlayer in teamEntity.Players.ToList())
                {
                    string lookupId = !string.IsNullOrWhiteSpace(teamPlayer.AccountId)
                        ? teamPlayer.AccountId
                        : teamPlayer.PlayerSteamId;

                    if (string.IsNullOrWhiteSpace(lookupId))
                        continue;

                    var apiResult = await ApiCourier.TryGetUserInfo(lookupId);
                    if (!apiResult.IsSuccess || apiResult.Data?.profile == null)
                        continue;

                    if (TryApplyTeamPlayerProfile(context, teamPlayer, apiResult.Data.profile, out string changeDescription))
                        result.ChangedPlayers.Add(changeDescription);
                }

                if (!string.IsNullOrWhiteSpace(teamEntity.TrainerSteamId))
                {
                    var trainerApi = await ApiCourier.TryGetUserInfo(teamEntity.TrainerSteamId);
                    if (trainerApi.IsSuccess && trainerApi.Data?.profile != null)
                    {
                        var trainerEntity = await context.Trainers
                            .FirstOrDefaultAsync(t => t.SteamId == teamEntity.TrainerSteamId);

                        if (trainerEntity != null &&
                            TryApplyTrainerProfile(trainerEntity, trainerApi.Data.profile, out _))
                        {
                            result.TrainerUpdated = true;
                            result.Trainer = ToUserModel(trainerEntity);
                        }
                    }
                }

                if (context.ChangeTracker.HasChanges())
                    await context.SaveChangesAsync();

                result.Team = MapTeam(teamEntity);
                return result;
            }
        }

        private static bool TryApplyTeamPlayerProfile(
            AppDbContext context,
            TeamPlayerEntity teamPlayer,
            DotaPlayerProfileInfo profile,
            out string changeDescription)
        {
            changeDescription = null;

            string newName = profile.personaname.ToString() ?? string.Empty;
            string newAvatar = profile.avatarfull.ToString() ?? string.Empty;
            string newAccountId = profile.account_id.ToString() ?? string.Empty;
            string newSteamId = profile.steamid.ToString() ?? string.Empty;

            bool nameChanged = !string.Equals(teamPlayer.Name ?? string.Empty, newName, StringComparison.Ordinal);
            bool avatarChanged = !string.Equals(teamPlayer.Avatar ?? string.Empty, newAvatar, StringComparison.Ordinal);

            if (!nameChanged && !avatarChanged)
                return false;

            string displayName = string.IsNullOrWhiteSpace(teamPlayer.Name) ? newName : teamPlayer.Name;
            changeDescription = nameChanged
                ? $"{displayName} → {newName}"
                : displayName;

            string oldSteamId = teamPlayer.PlayerSteamId;

            teamPlayer.Name = newName;
            teamPlayer.Avatar = newAvatar;

            if (!string.IsNullOrWhiteSpace(newAccountId))
                teamPlayer.AccountId = newAccountId;
            if (!string.IsNullOrWhiteSpace(newSteamId))
                teamPlayer.PlayerSteamId = newSteamId;

            var registeredPlayer = context.Players.FirstOrDefault(p =>
                p.SteamId == oldSteamId ||
                (!string.IsNullOrWhiteSpace(newSteamId) && p.SteamId == newSteamId));

            if (registeredPlayer != null)
            {
                registeredPlayer.Name = newName;
                registeredPlayer.Avatar = newAvatar;
                if (!string.IsNullOrWhiteSpace(newAccountId))
                    registeredPlayer.AccountId = newAccountId;
                if (!string.IsNullOrWhiteSpace(newSteamId))
                    registeredPlayer.SteamId = newSteamId;
            }

            return true;
        }

        private static bool TryApplyTrainerProfile(
            TrainerEntity trainer,
            DotaPlayerProfileInfo profile,
            out string changeDescription)
        {
            changeDescription = null;

            string newName = profile.personaname.ToString() ?? string.Empty;
            string newAvatar = profile.avatarfull.ToString() ?? string.Empty;
            string newAccountId = profile.account_id.ToString() ?? string.Empty;
            string newSteamId = profile.steamid.ToString() ?? string.Empty;

            bool nameChanged = !string.Equals(trainer.Name ?? string.Empty, newName, StringComparison.Ordinal);
            bool avatarChanged = !string.Equals(trainer.Avatar ?? string.Empty, newAvatar, StringComparison.Ordinal);

            if (!nameChanged && !avatarChanged)
                return false;

            string displayName = string.IsNullOrWhiteSpace(trainer.Name) ? newName : trainer.Name;
            changeDescription = nameChanged
                ? $"{displayName} → {newName}"
                : displayName;

            trainer.Name = newName;
            trainer.Avatar = newAvatar;

            if (!string.IsNullOrWhiteSpace(newAccountId))
                trainer.AccountId = newAccountId;
            if (!string.IsNullOrWhiteSpace(newSteamId))
                trainer.SteamId = newSteamId;

            return true;
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

            using (var context = CreateContext())
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (team.Name != newTeamName)
                    {
                        var teamEntity = await context.Teams.FindAsync(team.Id);
                        if (teamEntity != null)
                            teamEntity.Name = newTeamName;
                        team.Name = newTeamName;
                    }

                    var nonEmptyAccounts = newAccounts
                        .Where(a => !string.IsNullOrEmpty(a) && a != "0")
                        .ToList();

                    if (nonEmptyAccounts.Count != nonEmptyAccounts.Distinct().Count())
                        throw new Exception("Нельзя добавить одного и того же игрока дважды");

                    if (nonEmptyAccounts.Contains(team.TrainerSteamId))
                        throw new Exception("Тренер команды не может быть игроком");

                    foreach (var acc in nonEmptyAccounts)
                    {
                        if (await context.Trainers.AnyAsync(t => t.SteamId == acc))
                            throw new Exception("Нельзя добавить тренера как игрока");
                    }

                    for (int slot = 0; slot < 5; slot++)
                    {
                        string newAcc = newAccounts[slot];
                        UserModel existingPlayer = slot < team.Players.Count ? team.Players[slot] : null;

                        if (string.IsNullOrEmpty(newAcc) || newAcc == "0")
                        {
                            if (existingPlayer != null)
                            {
                                var toRemove = await context.TeamPlayers.FirstOrDefaultAsync(tp =>
                                    tp.TeamId == team.Id && tp.AccountId == existingPlayer.AccountID);

                                if (toRemove != null)
                                    context.TeamPlayers.Remove(toRemove);

                                team.Players[slot] = null;
                            }
                            continue;
                        }

                        if (existingPlayer != null && existingPlayer.AccountID == newAcc)
                            continue;

                        var apiResult = await ApiCourier.TryGetUserInfo(newAcc);
                        if (!apiResult.IsSuccess)
                            throw new Exception($"Игрок {newAcc} не найден");

                        var p = apiResult.Data.profile;
                        var user = new UserModel(
                            p.personaname.ToString(),
                            p.account_id.ToString(),
                            p.steamid.ToString(),
                            "",
                            p.avatarfull.ToString());

                        if (existingPlayer != null)
                        {
                            var entity = await context.TeamPlayers.FirstOrDefaultAsync(tp =>
                                tp.TeamId == team.Id && tp.AccountId == existingPlayer.AccountID);

                            if (entity != null)
                            {
                                entity.Name = user.Name;
                                entity.AccountId = user.AccountID;
                                entity.PlayerSteamId = user.SteamID;
                                entity.Avatar = user.Avatarfull;
                            }

                            team.Players[slot] = user;
                        }
                        else
                        {
                            context.TeamPlayers.Add(new TeamPlayerEntity
                            {
                                TeamId = team.Id,
                                Name = user.Name,
                                AccountId = user.AccountID,
                                PlayerSteamId = user.SteamID,
                                Avatar = user.Avatarfull
                            });

                            if (slot < team.Players.Count)
                                team.Players[slot] = user;
                            else
                                team.Players.Add(user);
                        }
                    }

                    team.Players = team.Players.Take(5).Where(p => p != null).ToList();

                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return team;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public static bool AddPlayerToTeam(int teamId, string playerSteamId)
        {
            using (var context = CreateContext())
            {
                var team = context.Teams.AsNoTracking().FirstOrDefault(t => t.Id == teamId);
                if (team == null)
                    return false;

                if (team.TrainerSteamId == playerSteamId)
                {
                    MessageBox.Show("Тренер не может быть участником команды");
                    return false;
                }

                if (context.TeamPlayers.Count(tp => tp.TeamId == teamId) >= 5)
                {
                    MessageBox.Show("В команде уже 5 игроков");
                    return false;
                }

                if (context.TeamPlayers.Any(tp =>
                        tp.TeamId == teamId && tp.PlayerSteamId == playerSteamId))
                {
                    MessageBox.Show("Игрок уже в этой команде");
                    return false;
                }

                context.TeamPlayers.Add(new TeamPlayerEntity
                {
                    TeamId = teamId,
                    PlayerSteamId = playerSteamId
                });

                return context.SaveChanges() > 0;
            }
        }

        public static bool RemovePlayerFromTeam(int teamId, string playerSteamId)
        {
            using (var context = CreateContext())
            {
                var entity = context.TeamPlayers.FirstOrDefault(tp =>
                    tp.TeamId == teamId && tp.PlayerSteamId == playerSteamId);

                if (entity == null)
                    return false;

                context.TeamPlayers.Remove(entity);
                return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Удаляет команду: записи в TeamPlayers, TrainingTasks и связанные строки удаляются каскадно.
        /// Учётные записи приложения в таблице Players не затрагиваются; игроки в других командах остаются.
        /// </summary>
        public static async Task<bool> DeleteTeamAsync(int teamId, string trainerSteamId)
        {
            try
            {
                using (var context = CreateContext())
                {
                    var team = await context.Teams
                        .FirstOrDefaultAsync(t => t.Id == teamId && t.TrainerSteamId == trainerSteamId);
                    if (team == null)
                        return false;

                    context.Teams.Remove(team);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка удаления команды", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Папка записей экрана для команды: BaseDirectory/records/{teamId}.
        /// </summary>
        public static bool TryDeleteTeamRecordingsFolder(int teamId)
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "records", teamId.ToString());
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region TrainingTasks

        public static async Task AddTrainingTaskAsync(TrainingTask task, int teamId)
        {
            using (var context = CreateContext())
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var entity = new TrainingTaskEntity
                    {
                        TeamId = teamId,
                        Title = task.Title,
                        Type = task.Type,
                        Metric = task.Metric,
                        TargetValue = task.TargetValue,
                        Comparison = task.Comparison,
                        Period = task.Period,
                        PeriodValue = task.PeriodValue,
                        StartDate = task.StartDate,
                        Deadline = task.Deadline,
                        IsCompleted = task.IsCompleted
                    };

                    foreach (var playerId in task.PlayerIds)
                    {
                        entity.AssignedPlayers.Add(new TrainingTaskPlayerEntity
                        {
                            PlayerId = playerId
                        });

                        entity.Progresses.Add(new TrainingTaskProgressEntity
                        {
                            PlayerId = playerId,
                            IsCompleted = false
                        });
                    }

                    context.TrainingTasks.Add(entity);
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public static async Task<List<TrainingTask>> GetTrainingTasksAsync(int teamId)
        {
            using (var context = CreateContext())
            {
                var entities = await context.TrainingTasks
                    .AsNoTracking()
                    .Include(t => t.AssignedPlayers)
                    .Include(t => t.Progresses)
                    .Where(t => t.TeamId == teamId)
                    .ToListAsync();

                return entities.Select(MapTrainingTask).ToList();
            }
        }

        public static async Task RemovePlayerFromTrainings(string playerId)
        {
            using (var context = CreateContext())
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var assignments = context.TrainingTaskPlayers
                        .Where(p => p.PlayerId == playerId);
                    context.TrainingTaskPlayers.RemoveRange(assignments);

                    var progresses = context.TrainingTaskProgresses
                        .Where(p => p.PlayerId == playerId);
                    context.TrainingTaskProgresses.RemoveRange(progresses);

                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public static async Task UpdatePlayerProgressAsync(int taskId, string playerId, bool isCompleted)
        {
            using (var context = CreateContext())
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var progress = await context.TrainingTaskProgresses
                        .FirstOrDefaultAsync(p => p.TaskId == taskId && p.PlayerId == playerId);

                    if (progress != null)
                        progress.IsCompleted = isCompleted;

                    bool allCompleted = !await context.TrainingTaskProgresses
                        .AnyAsync(p => p.TaskId == taskId && !p.IsCompleted);

                    var task = await context.TrainingTasks.FindAsync(taskId);
                    if (task != null)
                        task.IsCompleted = allCompleted;

                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public static async Task RemoveTrainingTaskAsync(int taskId)
        {
            using (var context = CreateContext())
            {
                var task = await context.TrainingTasks.FindAsync(taskId);
                if (task != null)
                {
                    context.TrainingTasks.Remove(task);
                    await context.SaveChangesAsync();
                }
            }
        }

        public static async Task ClearTrainingArchiveAsync(int teamId, DateTime archiveBorderDate)
        {
            using (var context = CreateContext())
            {
                var tasks = await context.TrainingTasks
                    .Where(t => t.TeamId == teamId && t.Deadline < archiveBorderDate)
                    .ToListAsync();

                context.TrainingTasks.RemoveRange(tasks);
                await context.SaveChangesAsync();
            }
        }

        #endregion

        #region Mapping

        private static UserModel ToUserModel(PlayerEntity entity)
        {
            return new UserModel(
                entity.Name ?? string.Empty,
                entity.AccountId ?? string.Empty,
                entity.SteamId ?? string.Empty,
                entity.Password ?? string.Empty,
                entity.Avatar ?? string.Empty);
        }

        private static UserModel ToUserModel(TrainerEntity entity)
        {
            return new UserModel(
                entity.Name ?? string.Empty,
                entity.AccountId ?? string.Empty,
                entity.SteamId ?? string.Empty,
                entity.Password ?? string.Empty,
                entity.Avatar ?? string.Empty);
        }

        private static UserModel ToUserModel(TeamPlayerEntity entity)
        {
            return new UserModel(
                entity.Name ?? string.Empty,
                entity.AccountId ?? string.Empty,
                entity.PlayerSteamId ?? string.Empty,
                string.Empty,
                entity.Avatar ?? string.Empty);
        }

        private static TeamModel MapTeam(TeamEntity team)
        {
            var model = new TeamModel(team.Id, team.Name, team.TrainerSteamId);
            foreach (var player in team.Players)
                model.Players.Add(ToUserModel(player));
            return model;
        }

        private static TrainingTask MapTrainingTask(TrainingTaskEntity entity)
        {
            var task = new TrainingTask
            {
                Id = entity.Id,
                TeamId = entity.TeamId,
                Title = entity.Title,
                Type = entity.Type,
                Metric = entity.Metric,
                TargetValue = entity.TargetValue,
                Comparison = entity.Comparison,
                Period = entity.Period,
                PeriodValue = entity.PeriodValue,
                StartDate = entity.StartDate,
                Deadline = entity.Deadline,
                IsCompleted = entity.IsCompleted,
                PlayerIds = entity.AssignedPlayers.Select(p => p.PlayerId).ToList(),
                CompletedPlayers = new Dictionary<string, bool>()
            };

            foreach (var progress in entity.Progresses)
            {
                if (!string.IsNullOrEmpty(progress.PlayerId))
                    task.CompletedPlayers[progress.PlayerId] = progress.IsCompleted;
            }

            return task;
        }

        #endregion
    }
}
