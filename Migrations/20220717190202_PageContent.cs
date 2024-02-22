using Microsoft.EntityFrameworkCore.Migrations;

namespace HorseService.Migrations
{
    public partial class PageContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentAr",
                table: "PageContents");

            migrationBuilder.DropColumn(
                name: "PageTitleAr",
                table: "PageContents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentAr",
                table: "PageContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PageTitleAr",
                table: "PageContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
