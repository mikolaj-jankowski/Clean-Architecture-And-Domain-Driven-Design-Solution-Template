using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrderDomain5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntergrationEvent");

            migrationBuilder.CreateTable(
                name: "IntegrationEvent",
                columns: table => new
                {
                    IntergrationEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccuredAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationEvent", x => x.IntergrationEventId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntegrationEvent");

            migrationBuilder.CreateTable(
                name: "IntergrationEvent",
                columns: table => new
                {
                    IntergrationEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccuredAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntergrationEvent", x => x.IntergrationEventId);
                });
        }
    }
}
