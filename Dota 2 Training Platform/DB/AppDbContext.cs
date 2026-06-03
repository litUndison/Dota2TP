using Dota_2_Training_Platform.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Dota_2_Training_Platform.DB
{
    public class AppDbContext : DbContext
    {
        private const string DefaultConnection =
            "Data Source=TrainingPlatform.db";

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<PlayerEntity> Players { get; set; }
        public DbSet<TrainerEntity> Trainers { get; set; }
        public DbSet<TeamEntity> Teams { get; set; }
        public DbSet<TeamPlayerEntity> TeamPlayers { get; set; }
        public DbSet<TrainingTaskEntity> TrainingTasks { get; set; }
        public DbSet<TrainingTaskPlayerEntity> TrainingTaskPlayers { get; set; }
        public DbSet<TrainingTaskProgressEntity> TrainingTaskProgresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite(DefaultConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerEntity>()
                .HasIndex(x => x.SteamId)
                .IsUnique();

            modelBuilder.Entity<PlayerEntity>()
                .HasIndex(x => x.Login)
                .IsUnique();

            modelBuilder.Entity<TrainerEntity>()
                .HasIndex(x => x.SteamId)
                .IsUnique();

            modelBuilder.Entity<TrainerEntity>()
                .HasIndex(x => x.Login)
                .IsUnique();

            modelBuilder.Entity<TeamPlayerEntity>()
                .HasOne(x => x.Team)
                .WithMany(x => x.Players)
                .HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TeamPlayerEntity>()
                .HasIndex(x => new { x.TeamId, x.PlayerSteamId })
                .IsUnique();

            modelBuilder.Entity<TrainingTaskEntity>()
                .HasOne(x => x.Team)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TrainingTaskPlayerEntity>()
                .HasOne(x => x.Task)
                .WithMany(x => x.AssignedPlayers)
                .HasForeignKey(x => x.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TrainingTaskPlayerEntity>()
                .HasIndex(x => new { x.TaskId, x.PlayerId })
                .IsUnique();

            modelBuilder.Entity<TrainingTaskProgressEntity>()
                .ToTable("TrainingTaskProgress")
                .HasOne(x => x.Task)
                .WithMany(x => x.Progresses)
                .HasForeignKey(x => x.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TrainingTaskProgressEntity>()
                .HasIndex(x => new { x.TaskId, x.PlayerId })
                .IsUnique();
        }
    }
}
