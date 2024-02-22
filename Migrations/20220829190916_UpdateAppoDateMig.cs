using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HorseService.Migrations
{
    public partial class UpdateAppoDateMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppoimentsDates",
                columns: table => new
                {
                    AppoimentsDateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeTowill = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppoimentsDates", x => x.AppoimentsDateId);
                    table.ForeignKey(
                        name: "FK_AppoimentsDates_Appointments_AppointmentsId",
                        column: x => x.AppointmentsId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppoimentsDates_AppointmentsId",
                table: "AppoimentsDates",
                column: "AppointmentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppoimentsDates");
        }
    }
}
