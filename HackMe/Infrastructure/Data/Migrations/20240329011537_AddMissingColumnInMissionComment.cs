using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackMe.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingColumnInMissionComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MissionId",
                table: "MissionComment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MissionComment_MissionId",
                table: "MissionComment",
                column: "MissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionComment_Mission_MissionId",
                table: "MissionComment",
                column: "MissionId",
                principalTable: "Mission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionComment_Mission_MissionId",
                table: "MissionComment");

            migrationBuilder.DropIndex(
                name: "IX_MissionComment_MissionId",
                table: "MissionComment");

            migrationBuilder.DropColumn(
                name: "MissionId",
                table: "MissionComment");
        }
    }
}
