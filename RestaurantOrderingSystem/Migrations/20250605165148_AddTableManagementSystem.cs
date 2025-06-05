using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantOrderingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddTableManagementSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionX",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PositionY",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReservationName",
                table: "Tables",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationTime",
                table: "Tables",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServerId",
                table: "Tables",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmployeeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6369));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6371));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6373));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6374));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6542));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6548));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6550));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6551));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6553));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6555));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6557));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6559));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6560));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6562));

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "EmployeeCode", "FirstName", "HireDate", "IsActive", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "SRV001", "Ahmet", new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6484), true, "Yılmaz", null },
                    { 2, "SRV002", "Ayşe", new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6486), true, "Kaya", null },
                    { 3, "SRV003", "Mehmet", new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6488), true, "Öz", null }
                });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PositionX", "PositionY", "ReservationName", "ReservationTime", "ServerId", "Status" },
                values: new object[] { new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6509), 0, 0, null, null, 1, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PositionX", "PositionY", "ReservationName", "ReservationTime", "ServerId", "Status" },
                values: new object[] { new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6513), 0, 0, null, null, 1, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PositionX", "PositionY", "ReservationName", "ReservationTime", "ServerId", "Status" },
                values: new object[] { new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6515), 0, 0, null, null, 2, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PositionX", "PositionY", "ReservationName", "ReservationTime", "ServerId", "Status" },
                values: new object[] { new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6516), 0, 0, null, null, 3, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PositionX", "PositionY", "ReservationName", "ReservationTime", "ServerId", "Status" },
                values: new object[] { new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6518), 0, 0, null, null, null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Tables_ServerId",
                table: "Tables",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_EmployeeCode",
                table: "Servers",
                column: "EmployeeCode",
                unique: true,
                filter: "[EmployeeCode] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Servers_ServerId",
                table: "Tables",
                column: "ServerId",
                principalTable: "Servers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Servers_ServerId",
                table: "Tables");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropIndex(
                name: "IX_Tables_ServerId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "PositionX",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "PositionY",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "ReservationName",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "ReservationTime",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "ServerId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tables");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4017));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4019));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4021));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4023));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4207));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4209));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4211));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4213));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4215));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4217));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4219));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4221));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4223));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4225));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4175));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4177));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4179));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4180));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 5, 3, 23, 500, DateTimeKind.Local).AddTicks(4182));
        }
    }
}
