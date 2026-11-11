using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "booking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    UserCount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_booking_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "hotel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rate_plan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rate_plan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "room_category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MinUserCount = table.Column<int>(type: "int", nullable: false),
                    MaxUserCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rate_plan_room_category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatePlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rate_plan_room_category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rate_plan_room_category_rate_plan_RatePlanId",
                        column: x => x.RatePlanId,
                        principalTable: "rate_plan",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_rate_plan_room_category_room_category_RoomCategoryId",
                        column: x => x.RoomCategoryId,
                        principalTable: "room_category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "room",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room", x => x.Id);
                    table.ForeignKey(
                        name: "FK_room_hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "hotel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_room_room_category_RoomCategoryId",
                        column: x => x.RoomCategoryId,
                        principalTable: "room_category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "room_in_booking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArrivalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DepartureDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_in_booking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_room_in_booking_booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "booking",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_room_in_booking_room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "room",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_booking_UserId",
                table: "booking",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_rate_plan_room_category_RatePlanId",
                table: "rate_plan_room_category",
                column: "RatePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_rate_plan_room_category_RoomCategoryId",
                table: "rate_plan_room_category",
                column: "RoomCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_room_HotelId",
                table: "room",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_room_RoomCategoryId",
                table: "room",
                column: "RoomCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_room_in_booking_BookingId",
                table: "room_in_booking",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_room_in_booking_RoomId",
                table: "room_in_booking",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rate_plan_room_category");

            migrationBuilder.DropTable(
                name: "room_in_booking");

            migrationBuilder.DropTable(
                name: "rate_plan");

            migrationBuilder.DropTable(
                name: "booking");

            migrationBuilder.DropTable(
                name: "room");

            migrationBuilder.DropTable(
                name: "hotel");

            migrationBuilder.DropTable(
                name: "room_category");
        }
    }
}
