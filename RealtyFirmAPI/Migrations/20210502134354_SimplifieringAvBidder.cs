using Microsoft.EntityFrameworkCore.Migrations;

namespace RealtyFirmAPI.Migrations
{
    public partial class SimplifieringAvBidder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Bidders");

            migrationBuilder.DropColumn(
                name: "Google_Id",
                table: "Bidders");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Bidders");

            migrationBuilder.DropColumn(
                name: "Postal_Area",
                table: "Bidders");

            migrationBuilder.DropColumn(
                name: "Postal_Code",
                table: "Bidders");

            migrationBuilder.AddColumn<string>(
                name: "Postal_Area",
                table: "Brokers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Postal_Code",
                table: "Brokers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Postal_Area",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "Postal_Code",
                table: "Brokers");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Bidders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Google_Id",
                table: "Bidders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Bidders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Postal_Area",
                table: "Bidders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Postal_Code",
                table: "Bidders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
