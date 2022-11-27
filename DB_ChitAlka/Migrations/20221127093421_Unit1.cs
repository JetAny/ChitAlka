using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB_ChitAlka.Migrations
{
    public partial class Unit1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "Userlibraries",
                newName: "CarentSectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarentSectionId",
                table: "Userlibraries",
                newName: "SectionId");
        }
    }
}
