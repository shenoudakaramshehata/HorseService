using Microsoft.EntityFrameworkCore.Migrations;

namespace HorseService.Migrations
{
    public partial class PageContentSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PageContents",
                columns: new[] { "PageContentId", "ContentEn", "PageTitleEn" },
                values: new object[] { 1, "Hey", "About Us" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PageContents",
                keyColumn: "PageContentId",
                keyValue: 1);
        }
    }
}
