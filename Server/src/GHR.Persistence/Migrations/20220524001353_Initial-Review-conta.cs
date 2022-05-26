using Microsoft.EntityFrameworkCore.Migrations;

namespace GHR.Persistence.Migrations
{
    public partial class InitialReviewconta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_AspNetUsers_ContasId",
                table: "Funcionarios");

            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_ContasId",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "ContasId",
                table: "Funcionarios");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Funcionarios",
                newName: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_ContaId",
                table: "Funcionarios",
                column: "ContaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_AspNetUsers_ContaId",
                table: "Funcionarios",
                column: "ContaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_AspNetUsers_ContaId",
                table: "Funcionarios");

            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_ContaId",
                table: "Funcionarios");

            migrationBuilder.RenameColumn(
                name: "ContaId",
                table: "Funcionarios",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "ContasId",
                table: "Funcionarios",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_ContasId",
                table: "Funcionarios",
                column: "ContasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_AspNetUsers_ContasId",
                table: "Funcionarios",
                column: "ContasId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
