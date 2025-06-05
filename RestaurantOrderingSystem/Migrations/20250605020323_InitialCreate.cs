using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantOrderingSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    IsOccupied = table.Column<bool>(type: "bit", nullable: false),
                    OccupiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableId = table.Column<int>(type: "int", nullable: false),
                    OrderTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpecialInstructions = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpecialInstructions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4017), "Sıcak çorbalar", "Çorbalar" },
                    { 2, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4019), "Ana yemek çeşitleri", "Ana Yemekler" },
                    { 3, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4021), "Sıcak ve soğuk içecekler", "İçecekler" },
                    { 4, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4023), "Tatlı çeşitleri", "Tatlılar" }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "Capacity", "CreatedAt", "IsOccupied", "Number", "OccupiedAt" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4175), false, "Masa 1", null },
                    { 2, 4, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4177), false, "Masa 2", null },
                    { 3, 6, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4179), false, "Masa 3", null },
                    { 4, 2, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4180), false, "Masa 4", null },
                    { 5, 8, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4182), false, "Masa 5", null }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "IsAvailable", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4207), "Geleneksel mercimek çorbası", null, true, "Mercimek Çorbası", 25.00m, null },
                    { 2, 1, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4209), "Ev yapımı tavuk çorbası", null, true, "Tavuk Çorbası", 30.00m, null },
                    { 3, 2, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4211), "Baharatlarla marine edilmiş izgara tavuk", null, true, "Izgara Tavuk", 85.00m, null },
                    { 4, 2, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4213), "Ev yapımı köfte", null, true, "Köfte", 75.00m, null },
                    { 5, 2, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4215), "Günün taze balığı", null, true, "Balık Izgara", 120.00m, null },
                    { 6, 3, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4217), "Türk çayı", null, true, "Çay", 10.00m, null },
                    { 7, 3, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4219), "Türk kahvesi", null, true, "Kahve", 20.00m, null },
                    { 8, 3, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4221), "Soğuk kola", null, true, "Kola", 15.00m, null },
                    { 9, 4, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4223), "Antep fıstıklı baklava", null, true, "Baklava", 45.00m, null },
                    { 10, 4, new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4225), "Ev yapımı sütlaç", null, true, "Sütlaç", 35.00m, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_CategoryId",
                table: "MenuItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_MenuItemId",
                table: "OrderItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TableId",
                table: "Orders",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_Number",
                table: "Tables",
                column: "Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Tables");
        }
    }
}
