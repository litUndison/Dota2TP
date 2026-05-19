using Dota_2_Training_Platform.Models.Trainings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota_2_Training_Platform.Models.Entity
{
    public class TrainingTaskEntity
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public virtual TeamEntity Team { get; set; }
        public string Title { get; set; }
        public TrainingType Type { get; set; }
        public TrainingMetric Metric { get; set; }
        public int TargetValue { get; set; }
        public ComparisonType Comparison { get; set; }
        public TrainingPeriod Period { get; set; }
        public int PeriodValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public virtual ICollection<TrainingTaskPlayerEntity> AssignedPlayers { get; set; } =
            new List<TrainingTaskPlayerEntity>();
        public virtual ICollection<TrainingTaskProgressEntity> Progresses { get; set; } =
            new List<TrainingTaskProgressEntity>();
    }
}
