using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB_ChitAlka.Migrations
{
    public partial class Unit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarentSectionId",
                table: "Userlibraries",
                newName: "CurentSectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurentSectionId",
                table: "Userlibraries",
                newName: "CarentSectionId");
        }
    }
}
