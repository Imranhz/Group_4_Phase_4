using Group4Flight.Models.DomainModels;

namespace Group4Flight.Models.ViewModels
{
    public class FlightViewModel
    {
        public IEnumerable<Flight> Flights { get; set; } = new List<Flight>();
        public IEnumerable<Airline> Airlines { get; set; } = new List<Airline>();

        // From/To dropdown options (populated from DB)
        public List<string> FromOptions { get; set; } = new();
        public List<string> ToOptions { get; set; } = new();

        // Filter criteria (persisted in session)
        public string? From { get; set; }
        public string? To { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? CabinType { get; set; }

        // Predefined dropdown lists
        public static List<string> CabinTypes => new() { "Economy", "Business", "First Class" };
        public static List<string> AircraftTypes => new() { "Boeing 737", "Boeing 747", "Airbus A320", "Airbus A380", "Embraer E190" };
    }
}
