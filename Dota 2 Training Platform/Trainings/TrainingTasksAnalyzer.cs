using DataBaseManager;
using Dota_2_Training_Platform.Models;
using Dota_2_Training_Platform.Models.Trainings;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota_2_Training_Platform.Trainings
{
    public static class TrainingTasksAnalyzer
    {
        #region Compare
        public static bool Compare(int value, int target, ComparisonType comparison)
        {
            switch (comparison)
            {
                case ComparisonType.GreaterOrEqual: return value >= target;
                case ComparisonType.LessOrEqual: return value <= target;
                case ComparisonType.Greater: return value > target;
                case ComparisonType.Less: return value < target;
                case ComparisonType.Equal: return value == target;
                default: return false;
            }
        }
        #endregion

        #region GetMetricValue
        public static int GetMetricValue(MatchPlayerModel player, TrainingMetric metric)
        {
            switch (metric)
            {
                case TrainingMetric.Kills: return player.kills;
                case TrainingMetric.Assists: return player.assists;
                case TrainingMetric.Deaths: return player.deaths;
                case TrainingMetric.GPM: return player.gold_per_min;
                case TrainingMetric.LastHits: return player.last_hits;
                case TrainingMetric.MatchesPlayed: return 1;
                default: return 0;
            }
        }
        #endregion

        #region Helper - получить игрока из матча
        private static MatchPlayerModel GetPlayer(DotaMatchDetailsModel match, string playerId)
        {
            long id = long.Parse(playerId);
            return match.players.FirstOrDefault(p => p.account_id == id);
        }
        #endregion

        #region CheckTrainingAsync
        public static async Task<bool> CheckTrainingAsync(TrainingTask task, string playerId, List<DotaMatchDetailsModel> matches)
        {
            if (matches == null || matches.Count == 0)
                return false;

            // фильтруем матчи по периоду задачи: от StartDate до Deadline
            var playerMatches = matches.Select(m => new
            {
                Match = m,
                Player = GetPlayer(m, playerId),
                MatchDate = DateTimeOffset.FromUnixTimeSeconds(m.start_time).DateTime
            })
            .Where(x => x.Player != null
                        && x.MatchDate >= task.StartDate   // используем StartDate
                        && x.MatchDate <= task.Deadline)
            .ToList();

            bool result = false;

            switch (task.Period)
            {
                case TrainingPeriod.SingleMatch:
                    result = playerMatches.Any(x =>
                        Compare(GetMetricValue(x.Player, task.Metric), task.TargetValue, task.Comparison));
                    break;

                case TrainingPeriod.MultipleMatches:
                    int successCount = playerMatches.Count(x =>
                        Compare(GetMetricValue(x.Player, task.Metric), task.TargetValue, task.Comparison));
                    result = successCount >= task.PeriodValue;
                    break;

                case TrainingPeriod.TimePeriod:
                    if (task.Metric == TrainingMetric.MatchesPlayed)
                    {
                        result = Compare(playerMatches.Count, task.TargetValue, task.Comparison);
                    }
                    else
                    {
                        result = playerMatches.Any(x =>
                            Compare(GetMetricValue(x.Player, task.Metric), task.TargetValue, task.Comparison));
                    }
                    break;
            }

            await dbManager.UpdatePlayerProgressAsync(task.Id, playerId, result);

            return result;
        }
        #endregion
    }
}
