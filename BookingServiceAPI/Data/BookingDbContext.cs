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

        public DbSet<SpecialRequest> SpecialRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpecialRequest>()
                .HasOne(sr => sr.Booking)
                .WithMany(b => b.SpecialRequests) // if you define this navigation
                .HasForeignKey(sr => sr.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
