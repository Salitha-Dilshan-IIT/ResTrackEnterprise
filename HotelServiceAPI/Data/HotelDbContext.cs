using HotelServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelServiceAPI.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure foreign key relationship
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ Configure precision for decimal
            modelBuilder.Entity<Room>()
                .Property(r => r.PricePerNight)
                .HasPrecision(10, 2); // Supports values like 12345678.99
        }
    }
}
