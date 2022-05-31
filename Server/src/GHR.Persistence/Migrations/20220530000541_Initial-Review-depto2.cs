using Microsoft.EntityFrameworkCore.Migrations;

namespace GHR.Persistence.Migrations
{
    public partial class InitialReviewdepto2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GerenteAdministrativo",
                table: "Departamentos");

            migrationBuilder.RenameColumn(
                name: "GerenteOperacioal",
                table: "Departamentos",
                newName: "Gerente");
        }
    }
}
