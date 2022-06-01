using Microsoft.EntityFrameworkCore.Migrations;

namespace GHR.Persistence.Migrations
{
    public partial class Initialreview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "Metas");

            migrationBuilder.DropColumn(
                name: "DiretorId",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "GerenteAdministrativoId",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "GerenteOperacionalId",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "Funcionarios");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Metas",
                newName: "Supervisor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Supervisor",
                table: "Metas",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "SupervisorId",
                table: "Metas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiretorId",
                table: "Funcionarios",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GerenteAdministrativoId",
                table: "Funcionarios",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GerenteOperacionalId",
                table: "Funcionarios",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupervisorId",
                table: "Funcionarios",
                type: "INTEGER",
                nullable: true);
        }
    }
}
