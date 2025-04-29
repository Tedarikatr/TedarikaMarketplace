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
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_SellerUsers_OwnerId",
                table: "Stores");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Stores",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Stores",
                newName: "StoreProvince");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Stores",
                newName: "StorePhone");

            migrationBuilder.RenameIndex(
                name: "IX_Stores_OwnerId",
                table: "Stores",
                newName: "IX_Stores_SellerId");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreDescription",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreDistrict",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreMail",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_SellerUsers_SellerId",
                table: "Stores",
                column: "SellerId",
                principalTable: "SellerUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_SellerUsers_SellerId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "StoreDescription",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "StoreDistrict",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "StoreMail",
                table: "Stores");

            migrationBuilder.RenameColumn(
                name: "StoreProvince",
                table: "Stores",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "StorePhone",
                table: "Stores",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Stores",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Stores_SellerId",
                table: "Stores",
                newName: "IX_Stores_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_SellerUsers_OwnerId",
                table: "Stores",
                column: "OwnerId",
                principalTable: "SellerUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
