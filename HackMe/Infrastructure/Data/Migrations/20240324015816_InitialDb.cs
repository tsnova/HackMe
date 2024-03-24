using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackMe.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agent",
                columns: table => new
                {
                    CodeName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Ranking = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsDecommissioned = table.Column<bool>(type: "bit", nullable: false),
                    PersonalData = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ActiveMission = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TotalSuccessfulMissions = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agent", x => x.CodeName);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DifficultyLevel = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    ExpectedResult = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeTask", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UrlKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsClassified = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentCodeName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    ChallangeTaskId = table.Column<int>(type: "int", nullable: false),
                    CompletedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChallengeResult_Agent_AgentCodeName",
                        column: x => x.AgentCodeName,
                        principalTable: "Agent",
                        principalColumn: "CodeName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChallengeResult_ChallengeTask_ChallangeTaskId",
                        column: x => x.ChallangeTaskId,
                        principalTable: "ChallengeTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeResult_AgentCodeName",
                table: "ChallengeResult",
                column: "AgentCodeName");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeResult_ChallangeTaskId",
                table: "ChallengeResult",
                column: "ChallangeTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChallengeResult");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Agent");

            migrationBuilder.DropTable(
                name: "ChallengeTask");
        }
    }
}
