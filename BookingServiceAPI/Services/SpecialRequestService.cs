using BookingServiceAPI.Dtos;
using BookingServiceAPI.Models;
using BookingServiceAPI.Repositories;

namespace BookingServiceAPI.Services
{
    public class SpecialRequestService
    {
        private readonly ISpecialRequestRepository _repo;

        public SpecialRequestService(ISpecialRequestRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<SpecialRequestDto> GetAll() =>
            _repo.GetAll().Select(r => new SpecialRequestDto
            {
                Id = r.Id,
                BookingId = r.BookingId,
                Description = r.Description
            });

        public SpecialRequestDto? GetById(int id)
        {
            var r = _repo.GetById(id);
            return r == null ? null : new SpecialRequestDto
            {
                Id = r.Id,
                BookingId = r.BookingId,
                Description = r.Description
            };
        }

        public IEnumerable<SpecialRequestDto> GetByBooking(int bookingId) =>
            _repo.GetByBookingId(bookingId).Select(r => new SpecialRequestDto
            {
                Id = r.Id,
                BookingId = r.BookingId,
                Description = r.Description
            });

        public void Create(CreateSpecialRequestDto dto)
        {
            var request = new SpecialRequest
            {
                BookingId = dto.BookingId,
                Description = dto.Description
            };
            _repo.Add(request);
        }

        public void Update(int id, CreateSpecialRequestDto dto)
        {
            var request = _repo.GetById(id);
            if (request == null) throw new ArgumentException("Request not found.");

            request.Description = dto.Description;
            _repo.Update(request);
        }

        public void Delete(int id) => _repo.Delete(id);
    }

}
