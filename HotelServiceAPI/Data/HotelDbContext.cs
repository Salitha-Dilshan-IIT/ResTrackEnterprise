using HotelServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelServiceAPI.Data
{
    public class HotelDbContext: DbContext
    {

        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
    }
}
