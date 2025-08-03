namespace BookingServiceAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int HotelId { get; set; } 
        public int RoomId { get; set; }
        public bool IsRecurring { get; set; }  // e.g. true/false checkbox
        public int? RecurringDaysInterval { get; set; }  // e.g. repeat every 7 days
        public int? RecurringCount { get; set; }  // number of times to repeat
    }
}
