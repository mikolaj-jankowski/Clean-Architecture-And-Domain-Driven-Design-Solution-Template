using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AnotherMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address_Country",
                table: "Customer",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_FlatNumber",
                table: "Customer",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_HouseNumber",
                table: "Customer",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_PostalCode",
                table: "Customer",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Customer",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_Country",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Address_FlatNumber",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Address_HouseNumber",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Address_PostalCode",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Customer");
        }
    }
}
