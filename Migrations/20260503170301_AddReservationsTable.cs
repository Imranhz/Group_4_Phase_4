using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Group4Flight.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    AirlineId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ImageName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.AirlineId);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightCode = table.Column<string>(type: "TEXT", nullable: false),
                    From = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    To = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DepartureTime = table.Column<long>(type: "INTEGER", nullable: false),
                    ArrivalTime = table.Column<long>(type: "INTEGER", nullable: false),
                    CabinType = table.Column<string>(type: "TEXT", nullable: false),
                    Emission = table.Column<double>(type: "REAL", nullable: false),
                    AircraftType = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    AirlineId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                    table.ForeignKey(
                        name: "FK_Flights_Airlines_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airlines",
                        principalColumn: "AirlineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReservedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Airlines",
                columns: new[] { "AirlineId", "ImageName", "Name" },
                values: new object[,]
                {
                    { 1, "aircanada.png", "Air Canada" },
                    { 2, "westjet.png", "WestJet" },
                    { 3, "porter.png", "Porter Airlines" },
                    { 4, "flair.png", "Flair Airlines" }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "AircraftType", "AirlineId", "ArrivalTime", "CabinType", "Date", "DepartureTime", "Emission", "FlightCode", "From", "Price", "To" },
                values: new object[,]
                {
                    { 1, "Boeing 737", 1, 378000000000L, "Economy", new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 288000000000L, 120.5, "AC101", "Toronto", 299.99m, "Vancouver" },
                    { 2, "Airbus A320", 1, 783000000000L, "Business", new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 504000000000L, 140.0, "AC202", "Vancouver", 599.99m, "Toronto" },
                    { 3, "Boeing 737", 2, 477000000000L, "Economy", new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 270000000000L, 115.2, "WJ301", "Calgary", 249.99m, "Montreal" },
                    { 4, "Boeing 747", 2, 702000000000L, "Business", new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 576000000000L, 118.0, "WJ402", "Montreal", 479.99m, "Calgary" },
                    { 5, "Embraer E190", 3, 360000000000L, "Economy", new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 324000000000L, 45.0, "PD501", "Toronto", 129.99m, "Ottawa" },
                    { 6, "Embraer E190", 3, 684000000000L, "First Class", new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 648000000000L, 47.5, "PD602", "Ottawa", 279.99m, "Toronto" },
                    { 7, "Airbus A320", 4, 279000000000L, "Economy", new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 216000000000L, 60.299999999999997, "F7701", "Edmonton", 159.99m, "Vancouver" },
                    { 8, "Airbus A320", 1, 504000000000L, "Business", new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 414000000000L, 88.700000000000003, "AC303", "Toronto", 389.99m, "Halifax" },
                    { 9, "Boeing 737", 2, 792000000000L, "Economy", new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 720000000000L, 58.100000000000001, "WJ503", "Vancouver", 139.99m, "Edmonton" },
                    { 10, "Airbus A380", 1, 360000000000L, "First Class", new DateTime(2026, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 252000000000L, 150.0, "AC404", "Montreal", 899.99m, "Vancouver" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirlineId",
                table: "Flights",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FlightId",
                table: "Reservations",
                column: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Airlines");
        }
    }
}
