using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackathonWebsite.Migrations
{
    /// <inheritdoc />
    public partial class changed_name_of_id_in_hack_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyToHacks_Teams_UserId",
                table: "ApplyToHacks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ApplyToHacks",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplyToHacks_UserId",
                table: "ApplyToHacks",
                newName: "IX_ApplyToHacks_TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyToHacks_Teams_TeamId",
                table: "ApplyToHacks",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyToHacks_Teams_TeamId",
                table: "ApplyToHacks");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "ApplyToHacks",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplyToHacks_TeamId",
                table: "ApplyToHacks",
                newName: "IX_ApplyToHacks_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyToHacks_Teams_UserId",
                table: "ApplyToHacks",
                column: "UserId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
