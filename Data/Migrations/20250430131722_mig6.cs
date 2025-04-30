using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreMarketCoverages");

            migrationBuilder.CreateTable(
                name: "StoreMarketCountries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    DeliveryTimeFrame = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreMarketCountries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreMarketCountries_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreMarketCountries_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreMarketDistricts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    DeliveryTimeFrame = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreMarketDistricts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreMarketDistricts_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreMarketDistricts_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreMarketNeighborhoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    NeighborhoodId = table.Column<int>(type: "int", nullable: false),
                    DeliveryTimeFrame = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreMarketNeighborhoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreMarketNeighborhoods_Neighborhoods_NeighborhoodId",
                        column: x => x.NeighborhoodId,
                        principalTable: "Neighborhoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreMarketNeighborhoods_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreMarketProvinces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    DeliveryTimeFrame = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreMarketProvinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreMarketProvinces_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreMarketProvinces_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreMarketRegions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    DeliveryTimeFrame = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreMarketRegions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreMarketRegions_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreMarketRegions_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreMarketStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    DeliveryTimeFrame = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreMarketStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreMarketStates_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreMarketStates_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketCountries_CountryId",
                table: "StoreMarketCountries",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketCountries_StoreId",
                table: "StoreMarketCountries",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketDistricts_DistrictId",
                table: "StoreMarketDistricts",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketDistricts_StoreId",
                table: "StoreMarketDistricts",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketNeighborhoods_NeighborhoodId",
                table: "StoreMarketNeighborhoods",
                column: "NeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketNeighborhoods_StoreId",
                table: "StoreMarketNeighborhoods",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketProvinces_ProvinceId",
                table: "StoreMarketProvinces",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketProvinces_StoreId",
                table: "StoreMarketProvinces",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketRegions_RegionId",
                table: "StoreMarketRegions",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketRegions_StoreId",
                table: "StoreMarketRegions",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketStates_StateId",
                table: "StoreMarketStates",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketStates_StoreId",
                table: "StoreMarketStates",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreMarketCountries");

            migrationBuilder.DropTable(
                name: "StoreMarketDistricts");

            migrationBuilder.DropTable(
                name: "StoreMarketNeighborhoods");

            migrationBuilder.DropTable(
                name: "StoreMarketProvinces");

            migrationBuilder.DropTable(
                name: "StoreMarketRegions");

            migrationBuilder.DropTable(
                name: "StoreMarketStates");

            migrationBuilder.CreateTable(
                name: "StoreMarketCoverages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    NeighborhoodId = table.Column<int>(type: "int", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    RegionId = table.Column<int>(type: "int", nullable: true),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    CoverageLevel = table.Column<int>(type: "int", nullable: false),
                    DeliveryTimeFrame = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_StoreMarketCoverages_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreMarketCoverages_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
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
                name: "IX_StoreMarketCoverages_RegionId",
                table: "StoreMarketCoverages",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMarketCoverages_StoreId",
                table: "StoreMarketCoverages",
                column: "StoreId");
        }
    }
}
