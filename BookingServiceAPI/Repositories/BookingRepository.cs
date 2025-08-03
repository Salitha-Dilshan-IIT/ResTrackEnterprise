using BookingServiceAPI.Data;
using BookingServiceAPI.Models;

namespace BookingServiceAPI.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext _context;

        public BookingRepository(BookingDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Booking> GetAll() => _context.Bookings.ToList();

        public Booking GetById(int id) => _context.Bookings.Find(id);

        public void Add(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public void Update(Booking booking)
        {
            _context.Bookings.Update(booking);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var b = _context.Bookings.Find(id);
            if (b != null)
            {
                _context.Bookings.Remove(b);
                _context.SaveChanges();
            }
        }
    }

}
