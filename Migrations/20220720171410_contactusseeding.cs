using Microsoft.EntityFrameworkCore.Migrations;

namespace HorseService.Migrations
{
    public partial class contactusseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerAddress",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "ContactUs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instgram",
                table: "ContactUs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "ContactUs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "ContactUs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhatsApp",
                table: "ContactUs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddress",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lng",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "ContactUs",
                columns: new[] { "ContactUsId", "Address", "CompanyName", "Email", "Facebook", "Fax", "Instgram", "LinkedIn", "Mobile", "Tele", "Twitter", "WhatsApp" },
                values: new object[] { 1, "Kwait", "Codeware", "codeware@gmail.com", "https://www.facebook.com/", "d23", "https://www.instagram.com/", "https://www.linkedin.com/feed/", "01091117381", "2090555", "https://twitter.com/", "01091117381" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ContactUs",
                keyColumn: "ContactUsId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Caption",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "Instgram",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "WhatsApp",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "CustomerAddress",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddress",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lng",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
