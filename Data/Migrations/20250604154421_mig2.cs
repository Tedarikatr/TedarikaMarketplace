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
                name: "QuotationRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerUserId = table.Column<int>(type: "int", nullable: false),
                    StoreProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotationRequests_BuyerUsers_BuyerUserId",
                        column: x => x.BuyerUserId,
                        principalTable: "BuyerUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationRequests_StoreProducts_StoreProductId",
                        column: x => x.StoreProductId,
                        principalTable: "StoreProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotationResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuotationRequestId = table.Column<int>(type: "int", nullable: false),
                    SellerUserId = table.Column<int>(type: "int", nullable: false),
                    OfferedUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinOrderQuantity = table.Column<int>(type: "int", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RespondedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotationResponses_QuotationRequests_QuotationRequestId",
                        column: x => x.QuotationRequestId,
                        principalTable: "QuotationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationResponses_SellerUsers_SellerUserId",
                        column: x => x.SellerUserId,
                        principalTable: "SellerUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuotationRequests_BuyerUserId",
                table: "QuotationRequests",
                column: "BuyerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationRequests_StoreProductId",
                table: "QuotationRequests",
                column: "StoreProductId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationResponses_QuotationRequestId",
                table: "QuotationResponses",
                column: "QuotationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationResponses_SellerUserId",
                table: "QuotationResponses",
                column: "SellerUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuotationResponses");

            migrationBuilder.DropTable(
                name: "QuotationRequests");
        }
    }
}
