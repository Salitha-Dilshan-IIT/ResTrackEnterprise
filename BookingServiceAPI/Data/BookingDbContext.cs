using BookingServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingServiceAPI.Data
{
    public class BookingDbContext: DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
    }
}
