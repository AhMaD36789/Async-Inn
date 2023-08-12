using Async_Inn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Async_Inn.Data
{
    public class AsyncInnDBContext : IdentityDbContext<User>
    {
        public AsyncInnDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel() { ID = 1, Name = "Four Seasons", StreetAddress = "Street1", City = "Amman", Country = "Jordan", Phone = "0777777777", State = "5th circle" },
                new Hotel() { ID = 2, Name = "Flower", StreetAddress = "Street2", City = "Amman", Country = "Jordan", Phone = "0788888888", State = "6th circle" },
                new Hotel() { ID = 3, Name = "Amman", StreetAddress = "Street3", City = "Amman", Country = "Jordan", Phone = "0799999999", State = "7th circle" }
            );

            modelBuilder.Entity<Room>().HasData(
                new Room() { ID = 1, Name = "PogChamp", Layout = 1 },
                new Room() { ID = 2, Name = "PeepoPlanket", Layout = 2 },
                new Room() { ID = 3, Name = "YEP", Layout = 3 }
            );

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity() { ID = 1, Name = "Free soap" },
                new Amenity() { ID = 2, Name = "dining table" },
                new Amenity() { ID = 3, Name = "PS5" }
            );

            modelBuilder.Entity<RoomAmenity>().HasKey
                (key => new
                {
                    key.RoomID,
                    key.AmenityID
                });

            modelBuilder.Entity<HotelRoom>().HasKey
                (key => new
                {
                    key.HotelID,
                    key.RoomNumber
                });

            SeedRoles(modelBuilder, "DistrictManager", "create", "update", "delete", "read");
            SeedRoles(modelBuilder, "PropertyManager", "create", "update", "read");
            SeedRoles(modelBuilder, "Agent", "update", "read");
            SeedRoles(modelBuilder, "AnonymousUsers", "read");

        }
        private int id = 1;
        private void SeedRoles(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole()
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };
            modelBuilder.Entity<IdentityRole>().HasData(role);
            var RoleClaims = permissions.Select(permissions =>
            new IdentityRoleClaim<string>
            {
                Id = id++,
                RoleId = role.Id,
                ClaimType = "persmissions",
                ClaimValue = permissions
            }).ToArray();
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(RoleClaims);
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<Amenity> Amenities { get; set; }

    }
}
