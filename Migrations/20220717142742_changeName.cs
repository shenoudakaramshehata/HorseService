using Microsoft.EntityFrameworkCore.Migrations;

namespace HorseService.Migrations
{
    public partial class changeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BreakTypes",
                keyColumn: "breaktypesId",
                keyValue: 3,
                column: "Title",
                value: "Periodly");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BreakTypes",
                keyColumn: "breaktypesId",
                keyValue: 3,
                column: "Title",
                value: "Weekly");
        }
    }
}
