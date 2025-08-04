using BookingServiceAPI.Dtos;
using BookingServiceAPI.Models;
using BookingServiceAPI.Repositories;

namespace BookingServiceAPI.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _repo;
        private readonly ISpecialRequestRepository _specialRequestRepo;

        public BookingService(IBookingRepository repo, ISpecialRequestRepository specialRequestRepo)
        {
            _repo = repo;
            _specialRequestRepo = specialRequestRepo;
        }

        public IEnumerable<BookingDto> GetAll()
        {
            return _repo.GetAll()
                .Select(b => new BookingDto
                {
                    Id = b.Id,
                    CustomerName = b.CustomerName,
                    CheckInDate = b.CheckInDate,
                    CheckOutDate = b.CheckOutDate,
                    HotelId = b.HotelId,
                    RoomId = b.RoomId,
                    IsRecurring = b.IsRecurring
                });
        }

        public BookingDto? GetById(int id)
        {
            var b = _repo.GetById(id);
            return b == null ? null : new BookingDto
            {
                Id = b.Id,
                CustomerName = b.CustomerName,
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate,
                HotelId = b.HotelId,
                RoomId = b.RoomId,
                IsRecurring = b.IsRecurring
            };
        }

        public void Update(int id, CreateBookingDto dto)
        {
            var booking = _repo.GetById(id);
            if (booking == null) return;

            // Check if new room and date range cause overlap
            if (!IsRoomAvailable(dto.RoomId, dto.CheckInDate, dto.CheckOutDate, id))
                throw new InvalidOperationException("The room is not available for the selected dates.");


            booking.CustomerName = dto.CustomerName;
            booking.CheckInDate = dto.CheckInDate;
            booking.CheckOutDate = dto.CheckOutDate;
            booking.HotelId = dto.HotelId;
            booking.RoomId = dto.RoomId;

            _repo.Update(booking);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }

        public void Create(CreateBookingDto dto)
        {
            if (dto.IsRecurring)
            {
                HandleRecurringBooking(dto);
                return;
            }

            var booking = MapToBooking(dto);

            if (!IsRoomAvailable(booking.RoomId, booking.CheckInDate, booking.CheckOutDate))
                throw new InvalidOperationException("Room is not available.");

            _repo.Add(booking);
        }

        public async Task<WeeklyBookingReportDto> GenerateWeeklyReportAsync()
        {
            return await Task.Run(() =>
            {
                var startDate = DateTime.Today;
                var endDate = startDate.AddDays(6);

                var bookings = _repo.GetAll()
                    .Where(b => b.CheckInDate < endDate && b.CheckOutDate > startDate)
                    .Select(b => new BookingDetailDto
                    {
                        Id = b.Id,
                        CustomerName = b.CustomerName,
                        CheckInDate = b.CheckInDate,
                        CheckOutDate = b.CheckOutDate,
                        HotelId = b.HotelId,
                        RoomId = b.RoomId,
                        IsRecurring = b.IsRecurring,
                        SpecialRequests = _specialRequestRepo
                            .GetByBookingId(b.Id)
                            .Select(r => r.Description)
                            .ToList()
                    }).ToList();

                return new WeeklyBookingReportDto
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    Bookings = bookings
                };
            });
        }



        private void HandleRecurringBooking(CreateBookingDto dto)
        {
            var interval = dto.RecurringDaysInterval ?? throw new ArgumentException("Interval required");
            var count = dto.RecurringCount ?? throw new ArgumentException("Count required");

            for (int i = 0; i < count; i++)
            {
                var checkIn = dto.CheckInDate.AddDays(i * interval);
                var checkOut = dto.CheckOutDate.AddDays(i * interval);

                if (!IsRoomAvailable(dto.RoomId, checkIn, checkOut))
                    throw new InvalidOperationException($"Room unavailable on {checkIn:yyyy-MM-dd}");
            }

            for (int i = 0; i < count; i++)
            {
                var checkIn = dto.CheckInDate.AddDays(i * interval);
                var checkOut = dto.CheckOutDate.AddDays(i * interval);

                var newBooking = new Booking
                {
                    CustomerName = dto.CustomerName,
                    CheckInDate = checkIn,
                    CheckOutDate = checkOut,
                    RoomId = dto.RoomId,
                    HotelId = dto.HotelId,
                    IsRecurring = false
                };

                _repo.Add(newBooking);
            }
        }

        private bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut, int? excludeBookingId = null)
        {
            return !_repo.GetAll().Any(b =>
                b.RoomId == roomId &&
                b.Id != excludeBookingId && 
                checkIn < b.CheckOutDate &&
                checkOut > b.CheckInDate);
        }

        private Booking MapToBooking(CreateBookingDto dto)
        {
            return new Booking
            {
                CustomerName = dto.CustomerName,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                RoomId = dto.RoomId,
                HotelId = dto.HotelId,
                IsRecurring = dto.IsRecurring,
                RecurringCount = dto.RecurringCount,
                RecurringDaysInterval = dto.RecurringDaysInterval
            };
        }
    }

}
