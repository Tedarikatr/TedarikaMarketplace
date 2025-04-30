using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreMarketCoverages_StoreMarkets_StoreMarketId",
                table: "StoreMarketCoverages");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProductMarkets_Markets_MarketId",
                table: "StoreProductMarkets");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProductShippingRegions_Markets_MarketId",
                table: "StoreProductShippingRegions");

            migrationBuilder.DropTable(
                name: "StoreMarkets");

            migrationBuilder.DropTable(
                name: "Markets");

            migrationBuilder.DropIndex(
                name: "IX_StoreProductShippingRegions_MarketId",
                table: "StoreProductShippingRegions");

            migrationBuilder.DropIndex(
                name: "IX_StoreProductMarkets_MarketId",
                table: "StoreProductMarkets");

            migrationBuilder.DropColumn(
                name: "MarketId",
                table: "StoreProductShippingRegions");

            migrationBuilder.DropColumn(
                name: "MarketId",
                table: "StoreProductMarkets");

            migrationBuilder.RenameColumn(
                name: "StoreMarketId",
                table: "StoreMarketCoverages",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_StoreMarketCoverages_StoreMarketId",
                table: "StoreMarketCoverages",
                newName: "IX_StoreMarketCoverages_StoreId");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryTimeFrame",
                table: "StoreMarketCoverages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "StoreMarketCoverages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketCoverages_RegionId",
                table: "StoreMarketCoverages",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreMarketCoverages_Regions_RegionId",
                table: "StoreMarketCoverages",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreMarketCoverages_Stores_StoreId",
                table: "StoreMarketCoverages",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreMarketCoverages_Regions_RegionId",
                table: "StoreMarketCoverages");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreMarketCoverages_Stores_StoreId",
                table: "StoreMarketCoverages");

            migrationBuilder.DropIndex(
                name: "IX_StoreMarketCoverages_RegionId",
                table: "StoreMarketCoverages");

            migrationBuilder.DropColumn(
                name: "DeliveryTimeFrame",
                table: "StoreMarketCoverages");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "StoreMarketCoverages");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "StoreMarketCoverages",
                newName: "StoreMarketId");

            migrationBuilder.RenameIndex(
                name: "IX_StoreMarketCoverages_StoreId",
                table: "StoreMarketCoverages",
                newName: "IX_StoreMarketCoverages_StoreMarketId");

            migrationBuilder.AddColumn<int>(
                name: "MarketId",
                table: "StoreProductShippingRegions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarketId",
                table: "StoreProductMarkets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Markets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryTimeFrame = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MarketType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreMarkets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketId = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreMarkets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreMarkets_Markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreMarkets_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreProductShippingRegions_MarketId",
                table: "StoreProductShippingRegions",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProductMarkets_MarketId",
                table: "StoreProductMarkets",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarkets_MarketId",
                table: "StoreMarkets",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarkets_StoreId",
                table: "StoreMarkets",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreMarketCoverages_StoreMarkets_StoreMarketId",
                table: "StoreMarketCoverages",
                column: "StoreMarketId",
                principalTable: "StoreMarkets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProductMarkets_Markets_MarketId",
                table: "StoreProductMarkets",
                column: "MarketId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProductShippingRegions_Markets_MarketId",
                table: "StoreProductShippingRegions",
                column: "MarketId",
                principalTable: "Markets",
                principalColumn: "Id");
        }
    }
}
