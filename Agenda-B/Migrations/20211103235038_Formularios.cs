using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda_B.Migrations
{
    public partial class Formularios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Formularios_Personas_PacienteId",
                table: "Formularios");

            migrationBuilder.DropForeignKey(
                name: "FK_Formularios_Personas_UsuarioId",
                table: "Formularios");

            migrationBuilder.DropIndex(
                name: "IX_Formularios_UsuarioId",
                table: "Formularios");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Formularios");

            migrationBuilder.AlterColumn<int>(
                name: "PacienteId",
                table: "Formularios",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Formularios_Personas_PacienteId",
                table: "Formularios",
                column: "PacienteId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Formularios_Personas_PacienteId",
                table: "Formularios");

            migrationBuilder.AlterColumn<int>(
                name: "PacienteId",
                table: "Formularios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Formularios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Formularios_UsuarioId",
                table: "Formularios",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Formularios_Personas_PacienteId",
                table: "Formularios",
                column: "PacienteId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Formularios_Personas_UsuarioId",
                table: "Formularios",
                column: "UsuarioId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
