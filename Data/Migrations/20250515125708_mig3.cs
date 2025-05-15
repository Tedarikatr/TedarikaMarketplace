using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreProductMarkets");

            migrationBuilder.AddColumn<bool>(
                name: "BlockedByExportBan",
                table: "StoreProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "StoreCarriers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "StoreApiKey",
                table: "StoreCarriers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GTIPCode",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApiEndpoint",
                table: "Carriers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApiKey",
                table: "Carriers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarrierCode",
                table: "Carriers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarrierLogoUrl",
                table: "Carriers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IntegrationType",
                table: "Carriers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Carriers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ProductExportBanned",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    GTIPCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsExportBanned = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductExportBanned", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductExportBanned_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductExportBanned_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductExportBanned_CountryId",
                table: "ProductExportBanned",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductExportBanned_ProductId",
                table: "ProductExportBanned",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductExportBanned");

            migrationBuilder.DropColumn(
                name: "BlockedByExportBan",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "StoreCarriers");

            migrationBuilder.DropColumn(
                name: "StoreApiKey",
                table: "StoreCarriers");

            migrationBuilder.DropColumn(
                name: "GTIPCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ApiEndpoint",
                table: "Carriers");

            migrationBuilder.DropColumn(
                name: "ApiKey",
                table: "Carriers");

            migrationBuilder.DropColumn(
                name: "CarrierCode",
                table: "Carriers");

            migrationBuilder.DropColumn(
                name: "CarrierLogoUrl",
                table: "Carriers");

            migrationBuilder.DropColumn(
                name: "IntegrationType",
                table: "Carriers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Carriers");

            migrationBuilder.CreateTable(
                name: "StoreProductMarkets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProductMarkets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreProductMarkets_StoreProducts_StoreProductId",
                        column: x => x.StoreProductId,
                        principalTable: "StoreProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreProductMarkets_StoreProductId",
                table: "StoreProductMarkets",
                column: "StoreProductId");
        }
    }
}
