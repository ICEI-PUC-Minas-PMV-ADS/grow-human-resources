using Microsoft.EntityFrameworkCore.Migrations;

namespace GHR.Persistence.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Cargos_CargoId",
                table: "Funcionarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Login_Funcionarios_FuncionarioId",
                table: "Login");

            migrationBuilder.DropForeignKey(
                name: "FK_Supervisores_Funcionarios_FuncionarioId",
                table: "Supervisores");

            migrationBuilder.DropIndex(
                name: "IX_Supervisores_FuncionarioId",
                table: "Supervisores");

            migrationBuilder.DropIndex(
                name: "IX_Login_FuncionarioId",
                table: "Login");

            migrationBuilder.AlterColumn<int>(
                name: "CargoId",
                table: "Funcionarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoginId",
                table: "Funcionarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LoginId1",
                table: "Funcionarios",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupervisorId",
                table: "Funcionarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupervisorId1",
                table: "Funcionarios",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_LoginId1",
                table: "Funcionarios",
                column: "LoginId1");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_SupervisorId1",
                table: "Funcionarios",
                column: "SupervisorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Cargos_CargoId",
                table: "Funcionarios",
                column: "CargoId",
                principalTable: "Cargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Login_LoginId1",
                table: "Funcionarios",
                column: "LoginId1",
                principalTable: "Login",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Supervisores_SupervisorId1",
                table: "Funcionarios",
                column: "SupervisorId1",
                principalTable: "Supervisores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Cargos_CargoId",
                table: "Funcionarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Login_LoginId1",
                table: "Funcionarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Supervisores_SupervisorId1",
                table: "Funcionarios");

            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_LoginId1",
                table: "Funcionarios");

            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_SupervisorId1",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "LoginId1",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "SupervisorId1",
                table: "Funcionarios");

            migrationBuilder.AlterColumn<int>(
                name: "CargoId",
                table: "Funcionarios",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisores_FuncionarioId",
                table: "Supervisores",
                column: "FuncionarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Login_FuncionarioId",
                table: "Login",
                column: "FuncionarioId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Cargos_CargoId",
                table: "Funcionarios",
                column: "CargoId",
                principalTable: "Cargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Login_Funcionarios_FuncionarioId",
                table: "Login",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisores_Funcionarios_FuncionarioId",
                table: "Supervisores",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
