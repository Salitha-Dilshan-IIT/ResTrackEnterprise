namespace BookingServiceAPI.Models
{
    public class SpecialRequest
    {
        public int Id { get; set; }               
        public int BookingId { get; set; }        // Foreign key
        public string Description { get; set; }

        public Booking Booking { get; set; }      // Navigation property (optional)
    }
}
