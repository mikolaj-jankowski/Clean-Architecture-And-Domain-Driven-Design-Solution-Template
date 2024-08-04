using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AnotherMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Customer",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Customer",
                newName: "Age");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailVerified",
                table: "Customer",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "IsEmailVerified",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Customer",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Customer",
                newName: "BirthDate");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Customer",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
