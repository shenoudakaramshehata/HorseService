using Microsoft.EntityFrameworkCore.Migrations;

namespace HorseService.Migrations
{
    public partial class PageContentSeeding3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PageContents",
                columns: new[] { "PageContentId", "ContentAr", "ContentEn", "PageTitleAr", "PageTitleEn" },
                values: new object[] { 1, "هاي", "Hey", "من نحن ", "About Us" });
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
