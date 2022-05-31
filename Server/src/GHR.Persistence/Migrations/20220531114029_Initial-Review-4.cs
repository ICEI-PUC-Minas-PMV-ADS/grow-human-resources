using Microsoft.EntityFrameworkCore.Migrations;

namespace GHR.Persistence.Migrations
{
    public partial class InitialReview4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "FuncionariosMetas");

            migrationBuilder.DropColumn(
                name: "ImagemURL",
                table: "Funcionarios");

            migrationBuilder.AddColumn<string>(
                name: "Supervisor",
                table: "FuncionariosMetas",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Supervisor",
                table: "FuncionariosMetas");

            migrationBuilder.AddColumn<int>(
                name: "SupervisorId",
                table: "FuncionariosMetas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagemURL",
                table: "Funcionarios",
                type: "TEXT",
                nullable: true);
        }
    }
}
