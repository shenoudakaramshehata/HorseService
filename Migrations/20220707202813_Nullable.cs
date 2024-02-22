using Microsoft.EntityFrameworkCore.Migrations;

namespace HorseService.Migrations
{
    public partial class Nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentStatuses_AppointmentStatusId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_PaymentMethods_PaymentMethodId",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentMethodId",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AppointmentStatusId",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppointmentStatuses_AppointmentStatusId",
                table: "Appointments",
                column: "AppointmentStatusId",
                principalTable: "AppointmentStatuses",
                principalColumn: "AppointmentStatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_PaymentMethods_PaymentMethodId",
                table: "Appointments",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "PaymentMethodId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentStatuses_AppointmentStatusId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_PaymentMethods_PaymentMethodId",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentMethodId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AppointmentStatusId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppointmentStatuses_AppointmentStatusId",
                table: "Appointments",
                column: "AppointmentStatusId",
                principalTable: "AppointmentStatuses",
                principalColumn: "AppointmentStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_PaymentMethods_PaymentMethodId",
                table: "Appointments",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "PaymentMethodId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
