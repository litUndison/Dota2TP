using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota_2_Training_Platform.Models.Trainings
{
    public enum TrainingType
    {
        Individual,
        Team
    }

    public enum TrainingMetric
    {
        Kills,
        Assists,
        Deaths,
        GPM,
        LastHits,
        MatchesPlayed
    }

    public enum TrainingPeriod
    {
        SingleMatch,
        MultipleMatches,
        TimePeriod
    }
    public enum ComparisonType
    {
        GreaterOrEqual,   // >=
        LessOrEqual,      // <=
        Greater,          // >
        Less,             // <
        Equal             // ==
    }
    public class TrainingTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TrainingType Type { get; set; }

        public List<string> PlayerIds { get; set; } = new List<string>();

        public TrainingMetric Metric { get; set; }
        public int TargetValue { get; set; }
        public ComparisonType Comparison { get; set; }

        public TrainingPeriod Period { get; set; }
        public int PeriodValue { get; set; }

        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
    }
}
