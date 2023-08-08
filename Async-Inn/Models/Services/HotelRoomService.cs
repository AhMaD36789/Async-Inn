using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    /// <summary>
    /// Represents a service for managing hotel room operations.
    /// </summary>
    public class HotelRoomService : IHotelRoom
    {
        private readonly AsyncInnDBContext _context;
        private readonly IRoom _room;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotelRoomService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="room">The room service.</param>
        public HotelRoomService(AsyncInnDBContext context, IRoom room)
        {
            _context = context;
            _room = room;
        }

        /// <summary>
        /// Creates a new hotel room entry and associates it with a hotel.
        /// </summary>
        /// <param name="hotelroom">The hotel room data to be created.</param>
        /// <param name="HotelID">The ID of the associated hotel.</param>
        /// <returns>The created hotel room data.</returns>
        public async Task<HotelRoomDTO> Create(HotelRoomDTO hotelroom, int HotelID)
        {
            HotelRoom NEWHotelRoom = new HotelRoom()
            {
                HotelID = HotelID,
                RoomNumber = hotelroom.RoomNumber,
                Rate = hotelroom.Rate,
                PetFriendly = hotelroom.PetFriendly,
                RoomID = hotelroom.RoomID
            };

            var room = await _room.GetRoom(hotelroom.RoomID);
            if (room != null)
            {
                hotelroom.Room = room;
                _context.Entry(NEWHotelRoom).State = EntityState.Added;
                await _context.SaveChangesAsync();
            }
            return hotelroom;
        }
        /// <summary>
        /// Deletes a hotel room based on the hotel and room number.
        /// </summary>
        /// <param name="HotelID">The ID of the hotel containing the room.</param>
        /// <param name="RoomNumber">The number of the room to be deleted.</param>

        public async Task Delete(int HotelID, int RoomNumber)
        {
            var hotelroom = await _context.HotelRooms
                .Where(x => x.HotelID == HotelID && x.RoomNumber == RoomNumber)
                .FirstOrDefaultAsync();

            if (hotelroom != null)
            {
                _context.HotelRooms.Remove(hotelroom);
                await _context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Retrieves hotel room details based on the hotel and room number.
        /// </summary>
        /// <param name="HotelID">The ID of the hotel containing the room.</param>
        /// <param name="RoomNumber">The number of the room to retrieve.</param>
        /// <returns>The hotel room data.</returns>
        public async Task<HotelRoomDTO> GetHotelRoom(int HotelID, int RoomNumber)
        {
            var HotelRoom = await _context.HotelRooms
                .Select(hrDTO => new HotelRoomDTO()
                {
                    HotelID = hrDTO.HotelID,
                    RoomNumber = hrDTO.RoomNumber,
                    Rate = hrDTO.Rate,
                    PetFriendly = hrDTO.PetFriendly,
                    RoomID = hrDTO.RoomID,
                    Room = new RoomDTO()
                    {
                        ID = hrDTO.Room.ID,
                        Name = hrDTO.Room.Name,
                        Layout = hrDTO.Room.Layout,

                        Amenities = hrDTO.Room.RoomAminities.Select(amDTO => new AmenityDTO()
                        {
                            ID = amDTO.amenity.ID,
                            Name = amDTO.amenity.Name
                        }).ToList()
                    }
                }
                ).FirstOrDefaultAsync(htlRoom => htlRoom.RoomNumber == RoomNumber && htlRoom.HotelID == HotelID);

            return HotelRoom;
        }
        /// <summary>
        /// Retrieves a list of hotel rooms associated with a specific hotel.
        /// </summary>
        /// <param name="HotelID">The ID of the hotel containing the rooms.</param>
        /// <returns>A list of hotel room data.</returns>
        public async Task<List<HotelRoomDTO>> GetHotelRooms(int HotelID)
        {
            var HotelRooms = await _context.HotelRooms
                .Select(x => new HotelRoomDTO()
                {
                    HotelID = x.HotelID,
                    RoomNumber = x.RoomNumber,
                    Rate = x.Rate,
                    PetFriendly = x.PetFriendly,
                    RoomID = x.RoomID,
                    Room = new RoomDTO()
                    {
                        ID = x.Room.ID,
                        Name = x.Room.Name,
                        Layout = x.Room.Layout,

                        Amenities = x.Room.RoomAminities.Select(y => new AmenityDTO()
                        {
                            ID = y.amenity.ID,
                            Name = y.amenity.Name
                        }).ToList()
                    }
                }
                ).ToListAsync();

            return HotelRooms;

        }
        /// <summary>
        /// Updates the details of a hotel room.
        /// </summary>
        /// <param name="HotelID">The ID of the hotel containing the room.</param>
        /// <param name="RoomNumber">The number of the room to update.</param>
        /// <param name="hotelroom">The updated hotel room data.</param>
        /// <returns>The updated hotel room data.</returns>
        public async Task<HotelRoomDTO> UpdateHotelRoom(int HotelID, int RoomNumber, HotelRoomDTO hotelroom)
        {
            var CurrentHotelRoom = await _context.HotelRooms.FindAsync(HotelID, RoomNumber);

            if (CurrentHotelRoom != null)
            {
                CurrentHotelRoom.HotelID = hotelroom.HotelID;

                CurrentHotelRoom.RoomNumber = hotelroom.RoomNumber;
                CurrentHotelRoom.RoomID = hotelroom.RoomID;
                CurrentHotelRoom.Rate = hotelroom.Rate;
                CurrentHotelRoom.PetFriendly = hotelroom.PetFriendly;
                _context.Entry(CurrentHotelRoom).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return hotelroom;
        }
    }
}
