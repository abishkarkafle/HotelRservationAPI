using HotelRservationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelRservationAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Reservation>  reservations { get; set; }
        public DbSet<Room> rooms { get; set; }
        public DbSet<Register> registers { get; set; }
        public DbSet<Customer> customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Register>()
                .HasKey(r => r.Username);
        }
    }
}
