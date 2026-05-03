using System.Text.Json;

namespace Group4Flight.Models.Utilities
{
    public class FlightCookie
    {
        private const string CookieKey = "FlightReservations";
        public List<int> ReservedFlightIds { get; private set; } = new();

        public FlightCookie(HttpRequest request)
        {
            var value = request.Cookies[CookieKey];
            if (value != null)
                ReservedFlightIds = JsonSerializer.Deserialize<List<int>>(value) ?? new();
        }

        public void AddReservation(int flightId, HttpResponse response)
        {
            if (!ReservedFlightIds.Contains(flightId))
                ReservedFlightIds.Add(flightId);
            Save(response);
        }

        public bool IsReserved(int flightId) => ReservedFlightIds.Contains(flightId);

        private void Save(HttpResponse response)
        {
            response.Cookies.Append(CookieKey,
                JsonSerializer.Serialize(ReservedFlightIds),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(14) });
        }
    }
}
