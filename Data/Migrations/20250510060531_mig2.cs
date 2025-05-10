using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreLocationCoverages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    RegionIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NeighborhoodIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreLocationCoverages", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreLocationCoverages");
        }
    }
}
