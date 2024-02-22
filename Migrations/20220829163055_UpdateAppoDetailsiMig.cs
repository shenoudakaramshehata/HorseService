using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HorseService.Migrations
{
    public partial class UpdateAppoDetailsiMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Services_ServiceId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ServiceId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Auth",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PostDate",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Ref",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TrackID",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TranID",
                table: "Appointments");

            migrationBuilder.CreateTable(
                name: "AppointmentDetails",
                columns: table => new
                {
                    AppointmentDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentsId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    AdditionalTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAdditionalCost = table.Column<double>(type: "float", nullable: false),
                    NumberOfHorses = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentDetails", x => x.AppointmentDetailsId);
                    table.ForeignKey(
                        name: "FK_AppointmentDetails_Appointments_AppointmentsId",
                        column: x => x.AppointmentsId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentDetails_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDetails_AppointmentsId",
                table: "AppointmentDetails",
                column: "AppointmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDetails_ServiceId",
                table: "AppointmentDetails",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentDetails");

            migrationBuilder.AddColumn<string>(
                name: "Auth",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostDate",
                table: "Appointments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ref",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackID",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TranID",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServiceId",
                table: "Appointments",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Services_ServiceId",
                table: "Appointments",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
