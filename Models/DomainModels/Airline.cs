namespace Group4Flight.Models.DomainModels
{
    public class Airline
    {
        public int AirlineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public ICollection<Flight> Flights { get; set; } = new List<Flight>();
    }
}
