using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreMarketCoverages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoverageLevel = table.Column<int>(type: "int", nullable: false),
                    StoreMarketId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    NeighborhoodId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreMarketCoverages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreMarketCoverages_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreMarketCoverages_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreMarketCoverages_Neighborhoods_NeighborhoodId",
                        column: x => x.NeighborhoodId,
                        principalTable: "Neighborhoods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreMarketCoverages_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreMarketCoverages_StoreMarkets_StoreMarketId",
                        column: x => x.StoreMarketId,
                        principalTable: "StoreMarkets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketCoverages_CountryId",
                table: "StoreMarketCoverages",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketCoverages_DistrictId",
                table: "StoreMarketCoverages",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketCoverages_NeighborhoodId",
                table: "StoreMarketCoverages",
                column: "NeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketCoverages_ProvinceId",
                table: "StoreMarketCoverages",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketCoverages_StoreMarketId",
                table: "StoreMarketCoverages",
                column: "StoreMarketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreMarketCoverages");
        }
    }
}
