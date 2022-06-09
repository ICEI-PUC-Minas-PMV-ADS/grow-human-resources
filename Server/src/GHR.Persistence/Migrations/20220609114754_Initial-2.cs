using Microsoft.EntityFrameworkCore.Migrations;

namespace GHR.Persistence.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartamentosId",
                table: "FuncionariosMetas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FuncionariosMetas_DepartamentosId",
                table: "FuncionariosMetas",
                column: "DepartamentosId");

            migrationBuilder.AddForeignKey(
                name: "FK_FuncionariosMetas_Departamentos_DepartamentosId",
                table: "FuncionariosMetas",
                column: "DepartamentosId",
                principalTable: "Departamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuncionariosMetas_Departamentos_DepartamentosId",
                table: "FuncionariosMetas");

            migrationBuilder.DropIndex(
                name: "IX_FuncionariosMetas_DepartamentosId",
                table: "FuncionariosMetas");

            migrationBuilder.DropColumn(
                name: "DepartamentosId",
                table: "FuncionariosMetas");
        }
    }
}
