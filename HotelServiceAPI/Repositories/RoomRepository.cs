using HotelServiceAPI.Data;
using HotelServiceAPI.Models;

namespace HotelServiceAPI.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _context;

        public RoomRepository(HotelDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetByHotel(int hotelId) =>
            _context.Rooms.Where(r => r.HotelId == hotelId).ToList();

        public Room Get(int id) => _context.Rooms.Find(id);

        public void Add(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public void Update(Room room)
        {
            _context.Rooms.Update(room);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var room = Get(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
            }
        }
    }
}
