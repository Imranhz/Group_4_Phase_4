using System.ComponentModel.DataAnnotations;
using Group4Flight.Models.Validation;
using Microsoft.AspNetCore.Mvc;

namespace Group4Flight.Models.DomainModels
{
    public class Flight
    {
        public int FlightId { get; set; }

        [Display(Name = "Flight Code")]
        [Required(ErrorMessage = "Flight Code is required.")]
        [RegularExpression(@"^[A-Za-z]{2}[0-9]{1,4}$",
            ErrorMessage = "Flight Code must start with 2 letters followed by 1-4 digits (e.g. AC101).")]
        public string FlightCode { get; set; } = string.Empty;

        [Display(Name = "From")]
        [Required(ErrorMessage = "From is required.")]
        [StringLength(50, ErrorMessage = "From cannot exceed 50 letters.")]
        [RegularExpression(@"^[A-Za-z\s]{1,50}$",
            ErrorMessage = "From must contain letters only (max 50).")]
        public string From { get; set; } = string.Empty;

        [Display(Name = "To")]
        [Required(ErrorMessage = "To is required.")]
        [StringLength(50, ErrorMessage = "To cannot exceed 50 letters.")]
        [RegularExpression(@"^[A-Za-z\s]{1,50}$",
            ErrorMessage = "To must contain letters only (max 50).")]
        public string To { get; set; } = string.Empty;

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        [FutureDate(MaxYearsAhead = 3,
            ErrorMessage = "Date must be after today and within {0} years from today.")]
        // Remote validation: Date + FlightCode combination must not already exist.
        // AdditionalFields passes the FlightCode value along with the Date being
        // validated (per course slide #52). TempData coordination happens in the
        // controller action so we don't duplicate this check on the server.
        [Remote(action: "VerifyFlightDate",
                controller: "Home",
                areaName: "Airline",
                // slide #52: AdditionalFields = comma-separated list of fields
                // the client will post along with the validated Date value.
                AdditionalFields = nameof(FlightCode) + "," + nameof(FlightId),
                ErrorMessage = "A flight with this Flight Code already exists on this date.")]
        public DateTime Date { get; set; }

        [Display(Name = "Departure Time")]
        [Required(ErrorMessage = "Departure Time is required.")]
        [DataType(DataType.Time)]
        // Regex allows HH:mm (what the HTML <input type="time"> posts) and also
        // HH:mm:ss (what TimeSpan.ToString() emits server-side).
        [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d(:[0-5]\d(\.\d+)?)?$",
            ErrorMessage = "Departure Time must fall within 24 hours and 60 minutes (HH:mm).")]
        public TimeSpan DepartureTime { get; set; }

        [Display(Name = "Arrival Time")]
        [Required(ErrorMessage = "Arrival Time is required.")]
        [DataType(DataType.Time)]
        [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d(:[0-5]\d(\.\d+)?)?$",
            ErrorMessage = "Arrival Time must fall within 24 hours and 60 minutes (HH:mm).")]
        public TimeSpan ArrivalTime { get; set; }

        [Display(Name = "Cabin Type")]
        [Required(ErrorMessage = "Cabin Type is required.")]
        public string CabinType { get; set; } = string.Empty;

        [Display(Name = "Emission (kg CO₂e)")]
        [Required(ErrorMessage = "Emission is required.")]
        [Range(0, 5000,
            ErrorMessage = "Emission must be between 0 and 5000 kg CO₂e.")]
        public double Emission { get; set; }

        [Display(Name = "Aircraft Type")]
        [Required(ErrorMessage = "Aircraft Type is required.")]
        public string AircraftType { get; set; } = string.Empty;

        [Display(Name = "Price (USD)")]
        [Required(ErrorMessage = "Price is required.")]
        [Range(typeof(decimal), "0", "50000",
            ErrorMessage = "Price must be between $0 and $50,000.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Airline")]
        [Required(ErrorMessage = "Please select an airline.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select an airline.")]
        public int AirlineId { get; set; }

        public Airline? Airline { get; set; }
    }
}
