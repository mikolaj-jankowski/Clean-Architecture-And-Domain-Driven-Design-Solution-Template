using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CA.And.DDD.Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedDbmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssemblyName",
                table: "IntegrationEvent",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AssemblyName",
                table: "DomainEvent",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssemblyName",
                table: "IntegrationEvent");

            migrationBuilder.DropColumn(
                name: "AssemblyName",
                table: "DomainEvent");
        }
    }
}
