using Microsoft.EntityFrameworkCore.Migrations;

namespace HorseService.Migrations
{
    public partial class UpdaetBridgeMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalTypeAppointments");

            migrationBuilder.CreateTable(
                name: "AppointmentAdditionalTypes",
                columns: table => new
                {
                    AppointmentAdditionalTypesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentsId = table.Column<int>(type: "int", nullable: false),
                    AdditionalTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentAdditionalTypes", x => x.AppointmentAdditionalTypesId);
                    table.ForeignKey(
                        name: "FK_AppointmentAdditionalTypes_AdditionalTypes_AdditionalTypeId",
                        column: x => x.AdditionalTypeId,
                        principalTable: "AdditionalTypes",
                        principalColumn: "AdditionalTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentAdditionalTypes_Appointments_AppointmentsId",
                        column: x => x.AppointmentsId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentAdditionalTypes_AdditionalTypeId",
                table: "AppointmentAdditionalTypes",
                column: "AdditionalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentAdditionalTypes_AppointmentsId",
                table: "AppointmentAdditionalTypes",
                column: "AppointmentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentAdditionalTypes");

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
    }
}
