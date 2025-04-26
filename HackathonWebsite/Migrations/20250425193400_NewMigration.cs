using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackathonWebsite.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyToTeams_Users_UserId",
                table: "ApplyToTeams");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ApplyToTeams",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyToTeams_Users_UserId",
                table: "ApplyToTeams",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyToTeams_Users_UserId",
                table: "ApplyToTeams");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ApplyToTeams",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyToTeams_Users_UserId",
                table: "ApplyToTeams",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
