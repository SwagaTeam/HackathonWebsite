using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackathonWebsite.Migrations
{
    /// <inheritdoc />
    public partial class links_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "Link",
                table: "Teams",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "LinkToGithub",
                table: "Teams",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LinkToGoogleDisk",
                table: "Teams",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LinkToGithub",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LinkToGoogleDisk",
                table: "Teams");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "Teams",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}
