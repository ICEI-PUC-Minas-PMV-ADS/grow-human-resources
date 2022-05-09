using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GHR.Persistence.Migrations
{
    public partial class AjusteMeta4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Metas_Supervisores_SupervisorId1",
                table: "Metas");

            migrationBuilder.DropIndex(
                name: "IX_Metas_SupervisorId1",
                table: "Metas");

            migrationBuilder.DropColumn(
                name: "SupervisorId1",
                table: "Metas");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataPromocao",
                table: "Supervisores",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UltimoCargo",
                table: "Supervisores",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataPromocao",
                table: "Supervisores");

            migrationBuilder.DropColumn(
                name: "UltimoCargo",
                table: "Supervisores");

            migrationBuilder.AddColumn<int>(
                name: "SupervisorId1",
                table: "Metas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Metas_SupervisorId1",
                table: "Metas",
                column: "SupervisorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Metas_Supervisores_SupervisorId1",
                table: "Metas",
                column: "SupervisorId1",
                principalTable: "Supervisores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
