using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RealtyFirmAPI.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bidders",
                columns: table => new
                {
                    Bidder_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Google_Id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bidders", x => x.Bidder_Id);
                });

            migrationBuilder.CreateTable(
                name: "Broker",
                columns: table => new
                {
                    Broker_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Google_Id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Broker", x => x.Broker_Id);
                });

            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    Listing_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Listing_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Room_Count = table.Column<int>(type: "int", nullable: false),
                    Listing_Price = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year_Built = table.Column<int>(type: "int", nullable: false),
                    Tour_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Floor_Area = table.Column<int>(type: "int", nullable: false),
                    Nonusable_Floor_Area = table.Column<int>(type: "int", nullable: false),
                    Lot_Area = table.Column<int>(type: "int", nullable: false),
                    Form_Of_Lease = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Broker_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.Listing_Id);
                    table.ForeignKey(
                        name: "FK_Listings_Broker_Broker_Id",
                        column: x => x.Broker_Id,
                        principalTable: "Broker",
                        principalColumn: "Broker_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Image_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Listing_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Image_Id);
                    table.ForeignKey(
                        name: "FK_Images_Listings_Listing_Id",
                        column: x => x.Listing_Id,
                        principalTable: "Listings",
                        principalColumn: "Listing_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ListingUsers",
                columns: table => new
                {
                    Listing_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Bidder_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Bid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingUsers", x => new { x.Bidder_Id, x.Listing_Id });
                    table.ForeignKey(
                        name: "FK_ListingUsers_Bidders_Bidder_Id",
                        column: x => x.Bidder_Id,
                        principalTable: "Bidders",
                        principalColumn: "Bidder_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingUsers_Listings_Listing_Id",
                        column: x => x.Listing_Id,
                        principalTable: "Listings",
                        principalColumn: "Listing_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_Listing_Id",
                table: "Images",
                column: "Listing_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_Broker_Id",
                table: "Listings",
                column: "Broker_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ListingUsers_Listing_Id",
                table: "ListingUsers",
                column: "Listing_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "ListingUsers");

            migrationBuilder.DropTable(
                name: "Bidders");

            migrationBuilder.DropTable(
                name: "Listings");

            migrationBuilder.DropTable(
                name: "Broker");
        }
    }
}
