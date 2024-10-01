using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surfs_Up.Migrations
{
    /// <inheritdoc />
    public partial class surfboard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingCatalogItem");

            migrationBuilder.DropTable(
                name: "CatalogItems");

            migrationBuilder.CreateTable(
                name: "Surfboards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<double>(type: "float", nullable: true),
                    Width = table.Column<double>(type: "float", nullable: true),
                    Thickness = table.Column<double>(type: "float", nullable: true),
                    Volume = table.Column<double>(type: "float", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Equipment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surfboards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookingSurfboard",
                columns: table => new
                {
                    BookingItemsId = table.Column<int>(type: "int", nullable: false),
                    BookingsBookingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingSurfboard", x => new { x.BookingItemsId, x.BookingsBookingId });
                    table.ForeignKey(
                        name: "FK_BookingSurfboard_Bookings_BookingsBookingId",
                        column: x => x.BookingsBookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingSurfboard_Surfboards_BookingItemsId",
                        column: x => x.BookingItemsId,
                        principalTable: "Surfboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingSurfboard_BookingsBookingId",
                table: "BookingSurfboard",
                column: "BookingsBookingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingSurfboard");

            migrationBuilder.DropTable(
                name: "Surfboards");

            migrationBuilder.CreateTable(
                name: "CatalogItems",
                columns: table => new
                {
                    CatalogItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Equipment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<double>(type: "float", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Thickness = table.Column<double>(type: "float", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: true),
                    Width = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItems", x => x.CatalogItemId);
                });

            migrationBuilder.CreateTable(
                name: "BookingCatalogItem",
                columns: table => new
                {
                    BookingItemsCatalogItemId = table.Column<int>(type: "int", nullable: false),
                    BookingsBookingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingCatalogItem", x => new { x.BookingItemsCatalogItemId, x.BookingsBookingId });
                    table.ForeignKey(
                        name: "FK_BookingCatalogItem_Bookings_BookingsBookingId",
                        column: x => x.BookingsBookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingCatalogItem_CatalogItems_BookingItemsCatalogItemId",
                        column: x => x.BookingItemsCatalogItemId,
                        principalTable: "CatalogItems",
                        principalColumn: "CatalogItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingCatalogItem_BookingsBookingId",
                table: "BookingCatalogItem",
                column: "BookingsBookingId");
        }
    }
}
