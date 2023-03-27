using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManager.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    AggregateRootId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProviderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.AggregateRootId);
                });

            migrationBuilder.CreateTable(
                name: "providers",
                columns: table => new
                {
                    AggregateRootId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_providers", x => x.AggregateRootId);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    orderitemid = table.Column<Guid>(name: "order_item_id", type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    orderid = table.Column<Guid>(name: "order_id", type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_items", x => x.orderitemid);
                    table.ForeignKey(
                        name: "FK_order_items_orders_order_id",
                        column: x => x.orderid,
                        principalTable: "orders",
                        principalColumn: "AggregateRootId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_ProviderId_Number",
                table: "orders",
                columns: new[] { "ProviderId", "Number" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "providers");

            migrationBuilder.DropTable(
                name: "orders");
        }
    }
}
