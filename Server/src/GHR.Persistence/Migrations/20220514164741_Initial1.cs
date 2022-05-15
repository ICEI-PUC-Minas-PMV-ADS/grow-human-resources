using Microsoft.EntityFrameworkCore.Migrations;

namespace GHR.Persistence.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "Departamentos");

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Cargos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Cargos");

            migrationBuilder.AddColumn<int>(
                name: "SupervisorId",
                table: "Departamentos",
                type: "INTEGER",
                nullable: true);
        }
    }
}
