using HotelServiceAPI.Models;

namespace HotelServiceAPI.Repositories
{
    public interface IHotelRepository
    {
        IEnumerable<Hotel> GetAll();
        Hotel GetById(int id);
        void Add(Hotel hotel);
        void Update(Hotel hotel);
        void Delete(int id);
        bool Exists(int id);
    }
}
