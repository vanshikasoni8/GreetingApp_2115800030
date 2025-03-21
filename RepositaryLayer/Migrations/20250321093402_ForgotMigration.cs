using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositaryLayer.Migrations
{
    public partial class ForgotMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetToken",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpiry",
                table: "User",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetToken",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpiry",
                table: "User");
        }
    }
}
