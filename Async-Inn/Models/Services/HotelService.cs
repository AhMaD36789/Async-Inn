using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services

{
    public class HotelService : IHotel
    {
        private readonly AsyncInnDBContext _context;

        public HotelService(AsyncInnDBContext context)
        {
            _context = context;
        }


        public async Task<Hotel> Create(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task Delete(int id)
        {
            Hotel hotel = await GetHotel(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<Hotel> GetHotel(int HotelId)
        {
            var Hotel = await _context.Hotels.FindAsync(HotelId);
            return Hotel;
        }

        public async Task<List<Hotel>> GetHotels()
        {
            return await _context.Hotels
                .Include(hotel => hotel.Rooms)
                .ThenInclude(rooms => rooms.Room)
                .ThenInclude(roomAmen => roomAmen.RoomAminities)
                .ThenInclude(amen => amen.amenity)
                .ToListAsync();
        }

        public async Task<Hotel> UpdateHotel(int id, Hotel UpdatedHotel)
        {
            Hotel CurrentHotel = await GetHotel(id);


            if (CurrentHotel != null)
            {

                CurrentHotel.Name = UpdatedHotel.Name;
                CurrentHotel.StreetAddress = UpdatedHotel.StreetAddress;
                CurrentHotel.City = UpdatedHotel.City;
                CurrentHotel.State = UpdatedHotel.State;
                CurrentHotel.Country = UpdatedHotel.Country;
                CurrentHotel.Phone = UpdatedHotel.Phone;
                await _context.SaveChangesAsync();

            }
            return CurrentHotel;

        }
    }
}
