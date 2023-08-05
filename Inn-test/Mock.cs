using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.Services;
using Async_Inn.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AsyncInnTest
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;


        protected readonly AsyncInnDBContext _db;
        protected readonly IRoom _room;

        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new AsyncInnDBContext(
                new DbContextOptionsBuilder<AsyncInnDBContext>()
                .UseSqlite(_connection).Options);


            _db.Database.EnsureCreated();

            _room = new RoomService(_db);
        }

        protected async Task<Amenity> CreateAndSaveAmenity()
        {
            Amenity amenity = new Amenity()
            {
                Name = "Test"
            };
            _db.Amenities.Add(amenity);
            await _db.SaveChangesAsync();

            return amenity;
        }

        protected async Task<Amenity> CreateAndSaveAmenity2()
        {
            Amenity amenity = new Amenity()
            {
                Name = "Test1"
            };
            _db.Amenities.Add(amenity);
            await _db.SaveChangesAsync();

            return amenity;
        }

        protected async Task<Room> CreateAndSaveRoom()
        {
            Room room = new Room()
            {
                Name = "Test",
                Layout = 1
            };
            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();
            return room;
        }
        protected async Task<Room> CreateAndSaveRoom2()
        {
            Room room = new Room()
            {
                Name = "Test1",
                Layout = 2
            };
            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();
            return room;
        }

        protected async Task<Hotel> CreateAndSaveHotel()
        {
            Hotel hotel = new Hotel()
            {
                Name = "Test",
                City = "city",
                Country = "testC",
                State = "tt",
                StreetAddress = "st",
                Phone = "0000"
            };

            _db.Hotels.Add(hotel);

            await _db.SaveChangesAsync();

            return hotel;

        }
        protected async Task<Hotel> CreateAndSaveHotel2()
        {
            Hotel hotel = new Hotel()
            {
                Name = "Asyncin",
                City = "Amman",
                Country = "Jordan",
                State = "Jr",
                StreetAddress = "st amman ",
                Phone = "0785785578"
            };

            _db.Hotels.Add(hotel);

            await _db.SaveChangesAsync();

            return hotel;

        }





        public void Dispose()
        {
            _db?.Dispose();

            _connection?.Dispose();
        }
    }
}