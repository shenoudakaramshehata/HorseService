using Microsoft.EntityFrameworkCore.Migrations;

namespace HorseService.Migrations
{
    public partial class contactseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PageContents",
                keyColumn: "PageContentId",
                keyValue: 1,
                columns: new[] { "ContentAr", "ContentEn" },
                values: new object[] { "هذا النص هو مثال لنص يمكن ان يستبدل في نفس المساحة", "This text is an example of text that can be replaced in the same space" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PageContents",
                keyColumn: "PageContentId",
                keyValue: 1,
                columns: new[] { "ContentAr", "ContentEn" },
                values: new object[] { "هاي", "Hey" });
        }
    }
}
