using Group4Flight.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Group4Flight.Models.DataLayer
{
    public class FlightContext : DbContext
    {
        public FlightContext(DbContextOptions<FlightContext> options) : base(options) { }

        public DbSet<Flight> Flights => Set<Flight>();
        public DbSet<Airline> Airlines => Set<Airline>();
        public DbSet<Reservation> Reservations => Set<Reservation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // SQLite does not natively support TimeSpan — store as long (ticks)
            modelBuilder.Entity<Flight>()
                .Property(f => f.DepartureTime)
                .HasConversion(
                    v => v.Ticks,
                    v => TimeSpan.FromTicks(v));

            modelBuilder.Entity<Flight>()
                .Property(f => f.ArrivalTime)
                .HasConversion(
                    v => v.Ticks,
                    v => TimeSpan.FromTicks(v));

            // Configure Reservation relationship
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Flight)
                .WithMany()
                .HasForeignKey(r => r.FlightId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Airlines
            modelBuilder.Entity<Airline>().HasData(
                new Airline { AirlineId = 1, Name = "Air Canada", ImageName = "aircanada.png" },
                new Airline { AirlineId = 2, Name = "WestJet", ImageName = "westjet.png" },
                new Airline { AirlineId = 3, Name = "Porter Airlines", ImageName = "porter.png" },
                new Airline { AirlineId = 4, Name = "Flair Airlines", ImageName = "flair.png" }
            );

            // Seed Flights
            modelBuilder.Entity<Flight>().HasData(
                new Flight
                {
                    FlightId = 1, FlightCode = "AC101", From = "Toronto", To = "Vancouver",
                    Date = new DateTime(2026, 5, 10), DepartureTime = new TimeSpan(8, 0, 0),
                    ArrivalTime = new TimeSpan(10, 30, 0), CabinType = "Economy",
                    Emission = 120.5, AircraftType = "Boeing 737", Price = 299.99m, AirlineId = 1
                },
                new Flight
                {
                    FlightId = 2, FlightCode = "AC202", From = "Vancouver", To = "Toronto",
                    Date = new DateTime(2026, 5, 15), DepartureTime = new TimeSpan(14, 0, 0),
                    ArrivalTime = new TimeSpan(21, 45, 0), CabinType = "Business",
                    Emission = 140.0, AircraftType = "Airbus A320", Price = 599.99m, AirlineId = 1
                },
                new Flight
                {
                    FlightId = 3, FlightCode = "WJ301", From = "Calgary", To = "Montreal",
                    Date = new DateTime(2026, 5, 12), DepartureTime = new TimeSpan(7, 30, 0),
                    ArrivalTime = new TimeSpan(13, 15, 0), CabinType = "Economy",
                    Emission = 115.2, AircraftType = "Boeing 737", Price = 249.99m, AirlineId = 2
                },
                new Flight
                {
                    FlightId = 4, FlightCode = "WJ402", From = "Montreal", To = "Calgary",
                    Date = new DateTime(2026, 5, 20), DepartureTime = new TimeSpan(16, 0, 0),
                    ArrivalTime = new TimeSpan(19, 30, 0), CabinType = "Business",
                    Emission = 118.0, AircraftType = "Boeing 747", Price = 479.99m, AirlineId = 2
                },
                new Flight
                {
                    FlightId = 5, FlightCode = "PD501", From = "Toronto", To = "Ottawa",
                    Date = new DateTime(2026, 5, 11), DepartureTime = new TimeSpan(9, 0, 0),
                    ArrivalTime = new TimeSpan(10, 0, 0), CabinType = "Economy",
                    Emission = 45.0, AircraftType = "Embraer E190", Price = 129.99m, AirlineId = 3
                },
                new Flight
                {
                    FlightId = 6, FlightCode = "PD602", From = "Ottawa", To = "Toronto",
                    Date = new DateTime(2026, 5, 11), DepartureTime = new TimeSpan(18, 0, 0),
                    ArrivalTime = new TimeSpan(19, 0, 0), CabinType = "First Class",
                    Emission = 47.5, AircraftType = "Embraer E190", Price = 279.99m, AirlineId = 3
                },
                new Flight
                {
                    FlightId = 7, FlightCode = "F7701", From = "Edmonton", To = "Vancouver",
                    Date = new DateTime(2026, 5, 18), DepartureTime = new TimeSpan(6, 0, 0),
                    ArrivalTime = new TimeSpan(7, 45, 0), CabinType = "Economy",
                    Emission = 60.3, AircraftType = "Airbus A320", Price = 159.99m, AirlineId = 4
                },
                new Flight
                {
                    FlightId = 8, FlightCode = "AC303", From = "Toronto", To = "Halifax",
                    Date = new DateTime(2026, 5, 14), DepartureTime = new TimeSpan(11, 30, 0),
                    ArrivalTime = new TimeSpan(14, 0, 0), CabinType = "Business",
                    Emission = 88.7, AircraftType = "Airbus A320", Price = 389.99m, AirlineId = 1
                },
                new Flight
                {
                    FlightId = 9, FlightCode = "WJ503", From = "Vancouver", To = "Edmonton",
                    Date = new DateTime(2026, 5, 22), DepartureTime = new TimeSpan(20, 0, 0),
                    ArrivalTime = new TimeSpan(22, 0, 0), CabinType = "Economy",
                    Emission = 58.1, AircraftType = "Boeing 737", Price = 139.99m, AirlineId = 2
                },
                new Flight
                {
                    FlightId = 10, FlightCode = "AC404", From = "Montreal", To = "Vancouver",
                    Date = new DateTime(2026, 5, 25), DepartureTime = new TimeSpan(7, 0, 0),
                    ArrivalTime = new TimeSpan(10, 0, 0), CabinType = "First Class",
                    Emission = 150.0, AircraftType = "Airbus A380", Price = 899.99m, AirlineId = 1
                }
            );
        }
    }
}
