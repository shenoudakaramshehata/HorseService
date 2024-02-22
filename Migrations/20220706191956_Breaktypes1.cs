using Microsoft.EntityFrameworkCore.Migrations;

namespace HorseService.Migrations
{
    public partial class Breaktypes1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BreakTypes_OffDays_OffDaysId",
                table: "BreakTypes");

            migrationBuilder.DropIndex(
                name: "IX_BreakTypes_OffDaysId",
                table: "BreakTypes");

            migrationBuilder.DropColumn(
                name: "OffDaysId",
                table: "BreakTypes");

            migrationBuilder.AddColumn<int>(
                name: "breaktypesId",
                table: "OffDays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OffDays_breaktypesId",
                table: "OffDays",
                column: "breaktypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_OffDays_BreakTypes_breaktypesId",
                table: "OffDays",
                column: "breaktypesId",
                principalTable: "BreakTypes",
                principalColumn: "breaktypesId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OffDays_BreakTypes_breaktypesId",
                table: "OffDays");

            migrationBuilder.DropIndex(
                name: "IX_OffDays_breaktypesId",
                table: "OffDays");

            migrationBuilder.DropColumn(
                name: "breaktypesId",
                table: "OffDays");

            migrationBuilder.AddColumn<int>(
                name: "OffDaysId",
                table: "BreakTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BreakTypes_OffDaysId",
                table: "BreakTypes",
                column: "OffDaysId");

            migrationBuilder.AddForeignKey(
                name: "FK_BreakTypes_OffDays_OffDaysId",
                table: "BreakTypes",
                column: "OffDaysId",
                principalTable: "OffDays",
                principalColumn: "OffDaysId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
