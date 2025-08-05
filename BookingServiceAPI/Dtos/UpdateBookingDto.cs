namespace BookingServiceAPI.Dtos
{
    public class UpdateBookingDto
    {
        public string CustomerName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
    }
}
