using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota_2_Training_Platform.Models.Entity
{
    public class TrainingTaskProgressEntity
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public virtual TrainingTaskEntity Task { get; set; }
        /// <summary>AccountId игрока из состава команды.</summary>
        public string PlayerId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
