using Microsoft.EntityFrameworkCore.Migrations;

namespace GHR.Persistence.Migrations
{
    public partial class InitialCT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarteiraTrablho",
                table: "DadosPessoais",
                newName: "CarteiraTrabalho");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarteiraTrabalho",
                table: "DadosPessoais",
                newName: "CarteiraTrablho");
        }
    }
}
