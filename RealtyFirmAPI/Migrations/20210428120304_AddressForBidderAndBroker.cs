using Microsoft.EntityFrameworkCore.Migrations;

namespace RealtyFirmAPI.Migrations
{
    public partial class AddressForBidderAndBroker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Google_Id",
                table: "Bidders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Postal_Area",
                table: "Bidders");

            migrationBuilder.DropColumn(
                name: "Postal_Code",
                table: "Bidders");

            migrationBuilder.AlterColumn<string>(
                name: "Google_Id",
                table: "Bidders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
