using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StateName",
                table: "StoreMarketStates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionName",
                table: "StoreMarketRegions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceName",
                table: "StoreMarketProvinces",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NeighborhoodName",
                table: "StoreMarketNeighborhoods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistrictName",
                table: "StoreMarketDistricts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "StoreMarketCountries",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StateName",
                table: "StoreMarketStates");

            migrationBuilder.DropColumn(
                name: "RegionName",
                table: "StoreMarketRegions");

            migrationBuilder.DropColumn(
                name: "ProvinceName",
                table: "StoreMarketProvinces");

            migrationBuilder.DropColumn(
                name: "NeighborhoodName",
                table: "StoreMarketNeighborhoods");

            migrationBuilder.DropColumn(
                name: "DistrictName",
                table: "StoreMarketDistricts");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "StoreMarketCountries");
        }
    }
}
