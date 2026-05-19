using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dota_2_Training_Platform.Migrations
{
    public partial class BaseMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    AccountId = table.Column<string>(nullable: true),
                    SteamId = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    TrainerSteamId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    AccountId = table.Column<string>(nullable: true),
                    SteamId = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AccountId = table.Column<string>(nullable: true),
                    PlayerSteamId = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamPlayers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Metric = table.Column<int>(nullable: false),
                    TargetValue = table.Column<int>(nullable: false),
                    Comparison = table.Column<int>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    PeriodValue = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingTasks_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingTaskPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingTaskPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingTaskPlayers_TrainingTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TrainingTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingTaskProgress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<string>(nullable: true),
                    IsCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingTaskProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingTaskProgress_TrainingTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TrainingTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_SteamId",
                table: "Players",
                column: "SteamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayers_TeamId_PlayerSteamId",
                table: "TeamPlayers",
                columns: new[] { "TeamId", "PlayerSteamId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_SteamId",
                table: "Trainers",
                column: "SteamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingTaskPlayers_TaskId_PlayerId",
                table: "TrainingTaskPlayers",
                columns: new[] { "TaskId", "PlayerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingTaskProgress_TaskId_PlayerId",
                table: "TrainingTaskProgress",
                columns: new[] { "TaskId", "PlayerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingTasks_TeamId",
                table: "TrainingTasks",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "TeamPlayers");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "TrainingTaskPlayers");

            migrationBuilder.DropTable(
                name: "TrainingTaskProgress");

            migrationBuilder.DropTable(
                name: "TrainingTasks");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
