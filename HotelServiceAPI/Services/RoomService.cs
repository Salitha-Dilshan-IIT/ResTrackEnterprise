using HotelServiceAPI.Models;
using HotelServiceAPI.Repositories;

namespace HotelServiceAPI.Services
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepo;
        private readonly IHotelRepository _hotelRepo;

        public RoomService(IRoomRepository roomRepo, IHotelRepository hotelRepo)
        {
            _roomRepo = roomRepo;
            _hotelRepo = hotelRepo;
        }

        public bool IsHotelValid(int hotelId)
        {
            return _hotelRepo.Exists(hotelId);
        }

        public void CreateRoom(Room room)
        {
            _roomRepo.Add(room);
        }

        public IEnumerable<Room> GetRoomsByHotel(int hotelId)
        {
            return _roomRepo.GetByHotel(hotelId);
        }

        public Room GetRoom(int id)
        {
            return _roomRepo.Get(id);
        }

        public void UpdateRoom(Room room)
        {
            _roomRepo.Update(room);
        }

        public void DeleteRoom(int id)
        {
            _roomRepo.Delete(id);
        }
    }
}
