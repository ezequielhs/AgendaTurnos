using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda_B.Migrations
{
    public partial class PacienteFormularios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "Formularios",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Formularios_PacienteId",
                table: "Formularios",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Formularios_Personas_PacienteId",
                table: "Formularios",
                column: "PacienteId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Formularios_Personas_PacienteId",
                table: "Formularios");

            migrationBuilder.DropIndex(
                name: "IX_Formularios_PacienteId",
                table: "Formularios");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Formularios");
        }
    }
}
