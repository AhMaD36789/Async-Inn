using Async_Inn.Models;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Data
{
    public class AsyncInnDBContext : DbContext
    {
        public AsyncInnDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel() { Id = 1, Name = "Four Seasons", StreetAddress = "Street1", City = "Amman", Country = "Jordan", Phone = "0777777777", State = "5th circle" },
                new Hotel() { Id = 2, Name = "Flower", StreetAddress = "Street2", City = "Amman", Country = "Jordan", Phone = "0788888888", State = "6th circle" },
                new Hotel() { Id = 3, Name = "Amman", StreetAddress = "Street3", City = "Amman", Country = "Jordan", Phone = "0799999999", State = "7th circle" }
            );

            modelBuilder.Entity<Room>().HasData(
                new Room() { Id = 1, Name = "PogChamp", Layout = 1 },
                new Room() { Id = 2, Name = "PeepoPlanket", Layout = 2 },
                new Room() { Id = 3, Name = "YEP", Layout = 3 }
            );

            modelBuilder.Entity<Aminity>().HasData(
                new Aminity() { Id = 1, Name = "Free soap" },
                new Aminity() { Id = 2, Name = "dining table" },
                new Aminity() { Id = 3, Name = "PS5" }
            );
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<RoomAminity> RoomAmenities { get; set; }
        public DbSet<Aminity> Amenities { get; set; }
    }
}
