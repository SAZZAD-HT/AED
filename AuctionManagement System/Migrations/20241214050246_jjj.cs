using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionManagement_System.Migrations
{
    /// <inheritdoc />
    public partial class jjj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buyers",
                columns: table => new
                {
                    BId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyers", x => x.BId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CID);
                });

            migrationBuilder.CreateTable(
                name: "DataSaveHeaders",
                columns: table => new
                {
                    DsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CID = table.Column<int>(type: "int", nullable: false),
                    SId = table.Column<int>(type: "int", nullable: false),
                    BId = table.Column<int>(type: "int", nullable: false),
                    DyeCond = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrackingNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CusPo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSaveHeaders", x => x.DsID);
                });

            migrationBuilder.CreateTable(
                name: "DataSaveRows",
                columns: table => new
                {
                    DsRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DsHeaderId = table.Column<int>(type: "int", nullable: false),
                    ItemNo = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    FinishType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpectedShipment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedShipmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FreeStock = table.Column<int>(type: "int", nullable: false),
                    seq = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSaveRows", x => x.DsRowId);
                });

            migrationBuilder.CreateTable(
                name: "ItemTables",
                columns: table => new
                {
                    ItemNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTables", x => x.ItemNo);
                });

            migrationBuilder.CreateTable(
                name: "ProductTables",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TKT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Shade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinishType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Reserve = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpectedShipment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedExpectedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrdStock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FreeStock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Seq = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTables", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "ShipTos",
                columns: table => new
                {
                    SId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipToCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipToName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipTos", x => x.SId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buyers");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "DataSaveHeaders");

            migrationBuilder.DropTable(
                name: "DataSaveRows");

            migrationBuilder.DropTable(
                name: "ItemTables");

            migrationBuilder.DropTable(
                name: "ProductTables");

            migrationBuilder.DropTable(
                name: "ShipTos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
