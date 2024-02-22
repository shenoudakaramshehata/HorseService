using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HorseService.Migrations
{
    public partial class Creditcard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Auth",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentID",
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

            migrationBuilder.AddColumn<bool>(
                name: "ispaid",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "payment_type",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Auth",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PaymentID",
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
                name: "TrackID",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TranID",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ispaid",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "payment_type",
                table: "Appointments");
        }
    }
}
