using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackathonWebsite.Migrations
{
    /// <inheritdoc />
    public partial class new_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyToHacks_Users_UserId",
                table: "ApplyToHacks");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyToHacks_Teams_UserId",
                table: "ApplyToHacks",
                column: "UserId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyToHacks_Teams_UserId",
                table: "ApplyToHacks");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyToHacks_Users_UserId",
                table: "ApplyToHacks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
