using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RealtyFirmAPI.Migrations
{
    public partial class FixedImageModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Listings_Listing_Id",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Broker_Broker_Id",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_ListingUsers_Bidders_Bidder_Id",
                table: "ListingUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ListingUsers_Listings_Listing_Id",
                table: "ListingUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListingUsers",
                table: "ListingUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Broker",
                table: "Broker");

            migrationBuilder.RenameTable(
                name: "ListingUsers",
                newName: "ListingBidders");

            migrationBuilder.RenameTable(
                name: "Broker",
                newName: "Brokers");

            migrationBuilder.RenameIndex(
                name: "IX_ListingUsers_Listing_Id",
                table: "ListingBidders",
                newName: "IX_ListingBidders_Listing_Id");

            migrationBuilder.AlterColumn<Guid>(
                name: "Listing_Id",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListingBidders",
                table: "ListingBidders",
                columns: new[] { "Bidder_Id", "Listing_Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brokers",
                table: "Brokers",
                column: "Broker_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Listings_Listing_Id",
                table: "Images",
                column: "Listing_Id",
                principalTable: "Listings",
                principalColumn: "Listing_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListingBidders_Bidders_Bidder_Id",
                table: "ListingBidders",
                column: "Bidder_Id",
                principalTable: "Bidders",
                principalColumn: "Bidder_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListingBidders_Listings_Listing_Id",
                table: "ListingBidders",
                column: "Listing_Id",
                principalTable: "Listings",
                principalColumn: "Listing_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Brokers_Broker_Id",
                table: "Listings",
                column: "Broker_Id",
                principalTable: "Brokers",
                principalColumn: "Broker_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Listings_Listing_Id",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_ListingBidders_Bidders_Bidder_Id",
                table: "ListingBidders");

            migrationBuilder.DropForeignKey(
                name: "FK_ListingBidders_Listings_Listing_Id",
                table: "ListingBidders");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Brokers_Broker_Id",
                table: "Listings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListingBidders",
                table: "ListingBidders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brokers",
                table: "Brokers");

            migrationBuilder.RenameTable(
                name: "ListingBidders",
                newName: "ListingUsers");

            migrationBuilder.RenameTable(
                name: "Brokers",
                newName: "Broker");

            migrationBuilder.RenameIndex(
                name: "IX_ListingBidders_Listing_Id",
                table: "ListingUsers",
                newName: "IX_ListingUsers_Listing_Id");

            migrationBuilder.AlterColumn<Guid>(
                name: "Listing_Id",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListingUsers",
                table: "ListingUsers",
                columns: new[] { "Bidder_Id", "Listing_Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Broker",
                table: "Broker",
                column: "Broker_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Listings_Listing_Id",
                table: "Images",
                column: "Listing_Id",
                principalTable: "Listings",
                principalColumn: "Listing_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Broker_Broker_Id",
                table: "Listings",
                column: "Broker_Id",
                principalTable: "Broker",
                principalColumn: "Broker_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListingUsers_Bidders_Bidder_Id",
                table: "ListingUsers",
                column: "Bidder_Id",
                principalTable: "Bidders",
                principalColumn: "Bidder_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListingUsers_Listings_Listing_Id",
                table: "ListingUsers",
                column: "Listing_Id",
                principalTable: "Listings",
                principalColumn: "Listing_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
