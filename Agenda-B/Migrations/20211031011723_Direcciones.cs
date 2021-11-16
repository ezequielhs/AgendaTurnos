using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda_B.Migrations
{
    public partial class Direcciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DireccionId",
                table: "Personas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DireccionId",
                table: "Personas",
                type: "int",
                nullable: true);
        }
    }
}
