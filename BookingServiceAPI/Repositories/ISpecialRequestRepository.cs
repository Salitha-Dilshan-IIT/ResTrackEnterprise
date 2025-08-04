using BookingServiceAPI.Models;

namespace BookingServiceAPI.Repositories
{
    public interface ISpecialRequestRepository
    {
        IEnumerable<SpecialRequest> GetAll();
        SpecialRequest GetById(int id);
        IEnumerable<SpecialRequest> GetByBookingId(int bookingId);
        void Add(SpecialRequest request);
        void Update(SpecialRequest request);
        void Delete(int id);
    }
}
