using Microsoft.EntityFrameworkCore.Migrations;

namespace HorseService.Migrations
{
    public partial class SeedingBreaktypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BreakTypes",
                columns: new[] { "breaktypesId", "Title" },
                values: new object[] { 1, "Hourly" });

            migrationBuilder.InsertData(
                table: "BreakTypes",
                columns: new[] { "breaktypesId", "Title" },
                values: new object[] { 2, "Daily" });

            migrationBuilder.InsertData(
                table: "BreakTypes",
                columns: new[] { "breaktypesId", "Title" },
                values: new object[] { 3, "Weekly" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BreakTypes",
                keyColumn: "breaktypesId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BreakTypes",
                keyColumn: "breaktypesId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BreakTypes",
                keyColumn: "breaktypesId",
                keyValue: 3);
        }
    }
}
