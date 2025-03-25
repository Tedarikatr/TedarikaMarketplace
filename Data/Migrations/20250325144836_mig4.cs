using System;
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
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "StoreProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "StoreProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategorySubName",
                table: "StoreProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "StoreProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "StoreProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnSale",
                table: "StoreProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxOrderQuantity",
                table: "StoreProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinOrderQuantity",
                table: "StoreProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "StoreProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specifications",
                table: "StoreProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreImageUrl",
                table: "StoreProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitType",
                table: "StoreProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitTypes",
                table: "StoreProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "StoreMarkets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionCode",
                table: "StoreMarkets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StoreProductRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitType = table.Column<int>(type: "int", nullable: false),
                    UnitTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinOrderQuantity = table.Column<int>(type: "int", nullable: false),
                    MaxOrderQuantity = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    AllowedDomestic = table.Column<bool>(type: "bit", nullable: false),
                    AllowedInternational = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CategorySubId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AdminNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProductRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreProductRequests_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreProductRequests_StoreId",
                table: "StoreProductRequests",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreProductRequests");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "CategorySubName",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "IsOnSale",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "MaxOrderQuantity",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "MinOrderQuantity",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "Specifications",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "StoreImageUrl",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "UnitType",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "UnitTypes",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "StoreMarkets");

            migrationBuilder.DropColumn(
                name: "RegionCode",
                table: "StoreMarkets");
        }
    }
}
