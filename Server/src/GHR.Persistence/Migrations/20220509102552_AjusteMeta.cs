using Microsoft.EntityFrameworkCore.Migrations;

namespace GHR.Persistence.Migrations
{
    public partial class AjusteMeta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MetaId",
                table: "Departamentos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "SiglaDepartamento",
                table: "Departamentos",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiglaDepartamento",
                table: "Departamentos");

            migrationBuilder.AlterColumn<int>(
                name: "MetaId",
                table: "Departamentos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
