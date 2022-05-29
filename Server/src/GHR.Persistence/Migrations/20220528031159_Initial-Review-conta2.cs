using Microsoft.EntityFrameworkCore.Migrations;

namespace GHR.Persistence.Migrations
{
    public partial class InitialReviewconta2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagemURL",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemURL",
                table: "AspNetUsers");
        }
    }
}
