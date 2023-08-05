using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
    public class RoomService : IRoom
    {
        private readonly AsyncInnDBContext _context;

        public RoomService(AsyncInnDBContext context)
        {
            _context = context;
        }


        public async Task<Room> Create(RoomDTO room)
        {
            Room newRoom = new Room()
            {
                ID = room.ID,
                Name = room.Name,
                Layout = room.Layout
            };

            _context.Rooms.Add(newRoom);
            await _context.SaveChangesAsync();
            room.ID = newRoom.ID;
            return newRoom;
        }

        public async Task Delete(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<RoomDTO> GetRoom(int roomId)
        {
            return await _context.Rooms
                .Select(x => new RoomDTO()
                {
                    ID = x.ID,
                    Name = x.Name,
                    Layout = x.Layout,
                    Amenities = x.RoomAminities
                   .Select(y => new AmenityDTO()
                   {
                       ID = y.amenity.ID,
                       Name = y.amenity.Name
                   }
                   ).ToList()
                }

                ).FirstOrDefaultAsync(z => z.ID == roomId);
        }

        public async Task<List<RoomDTO>> GetRooms()
        {
            return await _context.Rooms
                .Select(x => new RoomDTO()
                {
                    ID = x.ID,
                    Name = x.Name,
                    Layout = x.Layout,
                    Amenities = x.RoomAminities
                   .Select(y => new AmenityDTO()
                   {
                       ID = y.amenity.ID,
                       Name = y.amenity.Name
                   }
                   ).ToList()
                }

                ).ToListAsync();
        }


        public async Task<Room> UpdateRoom(int id, RoomDTO UpdatedRoom)
        {
            var CurrentRoom = await _context.Rooms.FindAsync(id);
            if (CurrentRoom != null)
            {
                CurrentRoom.Name = UpdatedRoom.Name;
                CurrentRoom.Layout = UpdatedRoom.Layout;
                _context.Entry(CurrentRoom).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return CurrentRoom;
        }
        public async Task<RoomAmenity> AddAmenityToRoom(int roomID, int amenityID)
        {
            var roomAmenity = new RoomAmenity()
            {
                RoomID = roomID,
                AmenityID = amenityID

            };
            _context.RoomAmenities.Add(roomAmenity);

            await _context.SaveChangesAsync();

            return roomAmenity;
        }
        public async Task RemoveAmenityFromoRoom(int roomID, int amenityID)
        {
            var del = await _context.RoomAmenities.FindAsync(roomID, amenityID);

            _context.Entry<RoomAmenity>(del).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }
    }
}