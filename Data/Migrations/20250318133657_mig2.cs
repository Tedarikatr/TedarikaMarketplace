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
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "AdminUsers",
                newName: "PasswordSalt");

            migrationBuilder.AddColumn<bool>(
                name: "IsSuperAdmin",
                table: "AdminUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "AdminUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSuperAdmin",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "AdminUsers");

            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "AdminUsers",
                newName: "Password");
        }
    }
}
