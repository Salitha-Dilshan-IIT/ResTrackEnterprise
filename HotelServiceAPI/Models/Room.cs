namespace HotelServiceAPI.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }

        public string Description { get; set; }

        public int HotelId { get; set; }

        public Hotel Hotel { get; set; }
    }
}
