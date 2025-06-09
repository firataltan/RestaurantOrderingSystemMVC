using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantOrderingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAuthenticationSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrentTableId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Tables_CurrentTableId",
                        column: x => x.CurrentTableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4569));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4571));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4573));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4574));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4741));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4743));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4745));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4747));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4749));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4751));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4752));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4754));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4757));

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4684));

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 2,
                column: "HireDate",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4686));

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 3,
                column: "HireDate",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4688));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4708));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4711));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4712));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4714));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(4716));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CurrentTableId", "Email", "FullName", "IsActive", "LastLoginAt", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(5058), null, "admin@restaurant.com", "Sistem Yöneticisi", true, null, "BJyF02Ub3WrfrsuyP1nqszZNtuI4akFT+LOa2twPSD0=", 3, "admin" },
                    { 2, new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(5077), null, "kitchen@restaurant.com", "Mutfak Şefi", true, null, "Vy1tWhzko9TBM9/sMO9Ud6q6kW+AjQqW5U34BM89vIg=", 2, "kitchen" },
                    { 3, new DateTime(2025, 6, 9, 1, 43, 57, 343, DateTimeKind.Local).AddTicks(5092), null, "user@restaurant.com", "Test Kullanıcısı", true, null, "KPfs1ETcAICf2bnT0CZw6qxxLdWYEzeKTp5WYLMCRQQ=", 1, "user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CurrentTableId",
                table: "Users",
                column: "CurrentTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Orders");

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

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6484));

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 2,
                column: "HireDate",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6486));

            migrationBuilder.UpdateData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 3,
                column: "HireDate",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6488));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6509));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6513));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6515));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6516));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 5, 19, 51, 47, 836, DateTimeKind.Local).AddTicks(6518));
        }
    }
}
