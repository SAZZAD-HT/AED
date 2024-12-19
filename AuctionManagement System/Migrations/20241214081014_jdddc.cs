using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionManagement_System.Migrations
{
    /// <inheritdoc />
    public partial class jdddc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangedExpectedDate",
                table: "ProductTables");

            migrationBuilder.DropColumn(
                name: "ExpectedShipment",
                table: "ProductTables");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ChangedExpectedDate",
                table: "ProductTables",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectedShipment",
                table: "ProductTables",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
