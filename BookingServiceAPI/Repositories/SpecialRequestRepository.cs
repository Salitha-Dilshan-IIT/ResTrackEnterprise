using BookingServiceAPI.Data;
using BookingServiceAPI.Models;

namespace BookingServiceAPI.Repositories
{
    public class SpecialRequestRepository : ISpecialRequestRepository
    {
        private readonly BookingDbContext _context;

        public SpecialRequestRepository(BookingDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SpecialRequest> GetAll() => _context.SpecialRequests.ToList();

        public SpecialRequest GetById(int id) => _context.SpecialRequests.Find(id);

        public IEnumerable<SpecialRequest> GetByBookingId(int bookingId) =>
            _context.SpecialRequests.Where(r => r.BookingId == bookingId).ToList();

        public void Add(SpecialRequest request)
        {
            _context.SpecialRequests.Add(request);
            _context.SaveChanges();
        }

        public void Update(SpecialRequest request)
        {
            _context.SpecialRequests.Update(request);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var request = _context.SpecialRequests.Find(id);
            if (request != null)
            {
                _context.SpecialRequests.Remove(request);
                _context.SaveChanges();
            }
        }
    }

}
