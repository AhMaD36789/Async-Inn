using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Async_Inn.Models.Services
{
    public class HotelRoomService : IHotelRoom
    {
        private readonly AsyncInnDBContext _context;

        public HotelRoomService(AsyncInnDBContext context)
        {
            _context = context;
        }

        public async Task<HotelRoom> Create(HotelRoom hotelroom, int HotelID)
        {
            var room = await _context.Rooms.FindAsync(hotelroom.RoomID);
            var hotel = await _context.Hotels.FindAsync(hotelroom.HotelID);

            hotelroom.HotelID = HotelID;

            hotelroom.Room = room;
            hotelroom.Hotel = hotel;

            _context.HotelRooms.Add(hotelroom);
            await _context.SaveChangesAsync();
            return hotelroom;
        }

        public async Task Delete(int HotelID, int RoomNumber)
        {
            HotelRoom hotelroom = await GetHotelRoom(HotelID, RoomNumber);
            if (hotelroom != null)
            {
                _context.HotelRooms.Remove(hotelroom);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<HotelRoom> GetHotelRoom(int HotelID, int RoomNumber)
        {
            var HotelRoom = await _context.HotelRooms
                .Include(x => x.Room)
                .ThenInclude(x => x.RoomAminities)
                .ThenInclude(x => x.amenity)
                .FirstOrDefaultAsync(x => x.HotelID == HotelID && x.RoomNumber == RoomNumber);
            return HotelRoom;
        }

        public async Task<List<HotelRoom>> GetHotelRooms(int HotelID)
        {
            return await _context.HotelRooms
                .Include(x => x.Room)
                .ThenInclude(x => x.RoomAminities)
                .ThenInclude(x => x.amenity)
                .Where(x => x.HotelID == HotelID)
                .ToListAsync();
        }

        public async Task<HotelRoom> UpdateHotelRoom(int HotelID, int RoomNumber, HotelRoom hotelroom)
        {
            HotelRoom CurrentHotelRoom = await GetHotelRoom(HotelID, RoomNumber);


            if (CurrentHotelRoom != null)
            {
                CurrentHotelRoom.RoomNumber = hotelroom.RoomNumber;
                CurrentHotelRoom.RoomID = hotelroom.RoomID;
                CurrentHotelRoom.Rate = hotelroom.Rate;
                CurrentHotelRoom.PetFriendly = hotelroom.PetFriendly;
                _context.Entry(CurrentHotelRoom).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return CurrentHotelRoom;
        }
    }
}