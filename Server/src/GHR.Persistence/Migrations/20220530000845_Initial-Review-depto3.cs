using Microsoft.EntityFrameworkCore.Migrations;

namespace GHR.Persistence.Migrations
{
    public partial class InitialReviewdepto3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GerenteAdministrativo",
                table: "Departamentos");

            migrationBuilder.RenameColumn(
                name: "GerenteOperacioal",
                table: "Departamentos",
                newName: "Gerente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gerente",
                table: "Departamentos",
                newName: "GerenteOperacioal");

            migrationBuilder.AddColumn<string>(
                name: "GerenteAdministrativo",
                table: "Departamentos",
                type: "TEXT",
                nullable: true);
        }
    }
}
