using HotelServiceAPI.Models;

namespace HotelServiceAPI.Repositories
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetByHotel(int hotelId);
        Room Get(int id);
        void Add(Room room);
        void Update(Room room);
        void Delete(int id);
    }

}
