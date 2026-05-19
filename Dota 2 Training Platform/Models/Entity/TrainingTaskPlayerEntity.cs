namespace Dota_2_Training_Platform.Models.Entity
{
    public class TrainingTaskPlayerEntity
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public virtual TrainingTaskEntity Task { get; set; }
        /// <summary>AccountId игрока из состава команды.</summary>
        public string PlayerId { get; set; }
    }
}
