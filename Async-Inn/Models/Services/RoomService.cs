using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
    /// <summary>
    /// Represents a service for managing room operations.
    /// </summary>
    public class RoomService : IRoom
    {
        private readonly AsyncInnDBContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public RoomService(AsyncInnDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new room.
        /// </summary>
        /// <param name="room">The room data to be created.</param>
        /// <returns>The created room data.</returns>

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

        /// <summary>
        /// Deletes a room based on its ID.
        /// </summary>
        /// <param name="id">The ID of the room to be deleted.</param>

        public async Task Delete(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrieves room details based on its ID.
        /// </summary>
        /// <param name="roomId">The ID of the room to retrieve.</param>
        /// <returns>The room data.</returns>
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

        /// <summary>
        /// Retrieves a list of all rooms.
        /// </summary>
        /// <returns>A list of room data.</returns>

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

        /// <summary>
        /// Updates the details of a room.
        /// </summary>
        /// <param name="id">The ID of the room to update.</param>
        /// <param name="UpdatedRoom">The updated room data.</param>
        /// <returns>The updated room data.</returns>

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

        /// <summary>
        /// Adds an amenity to a room.
        /// </summary>
        /// <param name="roomID">The ID of the room.</param>
        /// <param name="amenityID">The ID of the amenity.</param>
        /// <returns>The added room amenity data.</returns>

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

        /// <summary>
        /// Removes an amenity from a room.
        /// </summary>
        /// <param name="roomID">The ID of the room.</param>
        /// <param name="amenityID">The ID of the amenity.</param>

        public async Task RemoveAmenityFromoRoom(int roomID, int amenityID)
        {
            var del = await _context.RoomAmenities.FindAsync(roomID, amenityID);
            _context.Entry<RoomAmenity>(del).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}