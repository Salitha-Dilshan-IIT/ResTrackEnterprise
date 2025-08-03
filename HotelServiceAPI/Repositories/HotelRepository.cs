using HotelServiceAPI.Data;
using HotelServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelServiceAPI.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelDbContext _context;

        public HotelRepository(HotelDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Hotel> GetAll() => _context.Hotels.ToList();

        public Hotel GetById(int id) => _context.Hotels.Find(id);

        public void Add(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();
        }

        public void Update(Hotel hotel)
        {
            _context.Hotels.Update(hotel);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var hotel = _context.Hotels.Find(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                _context.SaveChanges();
            }
        }

        public bool Exists(int id) => _context.Hotels.Any(h => h.Id == id);
    }
}
