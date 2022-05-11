using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "RoomType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    IsBooked = table.Column<bool>(type: "bit", nullable: false),
                    IsOccupied = table.Column<bool>(type: "bit", nullable: false),
                    DateOfCheckIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfCheckOut = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RoomType",
                columns: new[] { "Id", "Type" },
                values: new object[] { 1, "Single" });

            migrationBuilder.InsertData(
                table: "RoomType",
                columns: new[] { "Id", "Type" },
                values: new object[] { 2, "Double" });

            migrationBuilder.InsertData(
                table: "RoomType",
                columns: new[] { "Id", "Type" },
                values: new object[] { 3, "Triple" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Number", "PricePerDay", "TypeId" },
                values: new object[,]
                {
                    { 1, "322", 750m, 3 },
                    { 2, "315", 550m, 2 },
                    { 3, "317", 750m, 3 },
                    { 4, "215", 250m, 1 },
                    { 5, "208", 750m, 3 },
                    { 6, "522", 550m, 2 },
                    { 7, "666", 250m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "DateOfCheckIn", "DateOfCheckOut", "IsBooked", "IsOccupied", "RoomId" },
                values: new object[] { 1, new DateTime(2022, 5, 8, 16, 14, 36, 267, DateTimeKind.Local).AddTicks(9509), new DateTime(2022, 7, 8, 16, 14, 36, 267, DateTimeKind.Local).AddTicks(9539), false, true, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_TypeId",
                table: "Rooms",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomType");
        }
    }
}
