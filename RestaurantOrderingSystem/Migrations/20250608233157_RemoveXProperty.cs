using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOrderingSystem.Migrations
{
    /// <inheritdoc />
    public partial class RemoveXProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderHistoryItems");

            migrationBuilder.DropTable(
                name: "OrderHistories");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1670));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1673));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1674));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1677));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1856));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1859));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1861));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1863));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1865));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1866));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1868));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1870));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1872));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1874));

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1796));

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 2,
                column: "HireDate",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1799));

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 3,
                column: "HireDate",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1801));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1822));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1825));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1827));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1828));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(1830));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(2114));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(2175));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 31, 57, 161, DateTimeKind.Local).AddTicks(2192));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    AnonymousId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHistories_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "OrderHistoryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    OrderHistoryId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SpecialInstructions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHistoryItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderHistoryItems_OrderHistories_OrderHistoryId",
                        column: x => x.OrderHistoryId,
                        principalTable: "OrderHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4158));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4160));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4162));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4164));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4566));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4568));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4570));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4573));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4624));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4627));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4628));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4630));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4631));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4633));

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4484));

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 2,
                column: "HireDate",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4487));

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 3,
                column: "HireDate",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4488));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4525));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4527));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4529));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4531));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(4532));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(5108));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(5134));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 2, 13, 28, 226, DateTimeKind.Local).AddTicks(5151));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_TableId",
                table: "OrderHistories",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_UserId",
                table: "OrderHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryItems_MenuItemId",
                table: "OrderHistoryItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryItems_OrderHistoryId",
                table: "OrderHistoryItems",
                column: "OrderHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
