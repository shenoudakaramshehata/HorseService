using Microsoft.EntityFrameworkCore.Migrations;

namespace HorseService.Migrations
{
    public partial class UpdaetAddTypeMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalTypes_Appointments_AppointmentsId",
                table: "AdditionalTypes");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalTypes_AppointmentsId",
                table: "AdditionalTypes");

            migrationBuilder.DropColumn(
                name: "AppointmentsId",
                table: "AdditionalTypes");

            migrationBuilder.CreateTable(
                name: "AdditionalTypeAppointments",
                columns: table => new
                {
                    AdditionalTypesAdditionalTypeId = table.Column<int>(type: "int", nullable: false),
                    AppointmentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalTypeAppointments", x => new { x.AdditionalTypesAdditionalTypeId, x.AppointmentsId });
                    table.ForeignKey(
                        name: "FK_AdditionalTypeAppointments_AdditionalTypes_AdditionalTypesAdditionalTypeId",
                        column: x => x.AdditionalTypesAdditionalTypeId,
                        principalTable: "AdditionalTypes",
                        principalColumn: "AdditionalTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdditionalTypeAppointments_Appointments_AppointmentsId",
                        column: x => x.AppointmentsId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalTypeAppointments_AppointmentsId",
                table: "AdditionalTypeAppointments",
                column: "AppointmentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalTypeAppointments");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentsId",
                table: "AdditionalTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalTypes_AppointmentsId",
                table: "AdditionalTypes",
                column: "AppointmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalTypes_Appointments_AppointmentsId",
                table: "AdditionalTypes",
                column: "AppointmentsId",
                principalTable: "Appointments",
                principalColumn: "AppointmentsId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
