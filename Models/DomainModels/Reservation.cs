using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group4Flight.Models.DomainModels
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [Required]
        public int FlightId { get; set; }

        [Required]
        public DateTime ReservedDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        // Foreign key relationship
        [ForeignKey("FlightId")]
        public Flight? Flight { get; set; }
    }
}
