namespace BookingServiceAPI.Dtos
{
    using System.Xml.Serialization;

    public class WeeklyBookingReportDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [XmlArray("Bookings")]
        [XmlArrayItem("Booking")]
        public List<BookingDetailDto> Bookings { get; set; }
    }

}
