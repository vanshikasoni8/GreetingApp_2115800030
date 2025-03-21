using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositaryLayer.Migrations
{
    public partial class ForignMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Greetings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Greetings_UserId",
                table: "Greetings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Greetings_User_UserId",
                table: "Greetings",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Greetings_User_UserId",
                table: "Greetings");

            migrationBuilder.DropIndex(
                name: "IX_Greetings_UserId",
                table: "Greetings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Greetings");
        }
    }
}
