using Microsoft.EntityFrameworkCore.Migrations;

namespace GHR.Persistence.Migrations
{
    public partial class Initialreview5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FuncionariosMetas",
                table: "FuncionariosMetas");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FuncionariosMetas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FuncionariosMetas",
                table: "FuncionariosMetas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionariosMetas_FuncionarioId",
                table: "FuncionariosMetas",
                column: "FuncionarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FuncionariosMetas",
                table: "FuncionariosMetas");

            migrationBuilder.DropIndex(
                name: "IX_FuncionariosMetas_FuncionarioId",
                table: "FuncionariosMetas");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FuncionariosMetas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FuncionariosMetas",
                table: "FuncionariosMetas",
                columns: new[] { "FuncionarioId", "MetaId" });
        }
    }
}
