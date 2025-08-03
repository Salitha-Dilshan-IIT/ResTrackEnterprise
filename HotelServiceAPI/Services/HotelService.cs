using HotelServiceAPI.Models;
using HotelServiceAPI.Repositories;

namespace HotelServiceAPI.Services
{
    public class HotelService
    {
        private readonly IHotelRepository _repository;

        public HotelService(IHotelRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Hotel> GetAll() => _repository.GetAll();

        public Hotel GetById(int id) => _repository.GetById(id);

        public void Create(Hotel hotel) => _repository.Add(hotel);

        public void Update(Hotel hotel) => _repository.Update(hotel);

        public void Delete(int id) => _repository.Delete(id);
    }
}
