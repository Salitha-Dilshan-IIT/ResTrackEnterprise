using System.Xml.Serialization;

namespace BookingServiceAPI.Dtos
{
    public class BookingDetailDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public bool IsRecurring { get; set; }

        [XmlArray("SpecialRequests")]
        [XmlArrayItem("Request")]
        public List<string> SpecialRequests { get; set; }
    }
}
