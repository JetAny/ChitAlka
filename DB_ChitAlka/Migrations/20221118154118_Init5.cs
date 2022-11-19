using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB_ChitAlka.Migrations
{
    public partial class Init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userlibrary_user_UserId",
                table: "userlibrary");

            migrationBuilder.DropIndex(
                name: "IX_userlibrary_UserId",
                table: "userlibrary");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "userlibrary");

            migrationBuilder.AddColumn<int>(
                name: "Userlibrary",
                table: "user",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Userlibrary",
                table: "user");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "userlibrary",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_userlibrary_UserId",
                table: "userlibrary",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_userlibrary_user_UserId",
                table: "userlibrary",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id");
        }
    }
}
