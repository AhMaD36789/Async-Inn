using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    /// <summary>
    /// Represents a service for managing hotel operations.
    /// </summary>
    public class HotelService : IHotel
    {
        private readonly AsyncInnDBContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotelService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public HotelService(AsyncInnDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new hotel entry.
        /// </summary>
        /// <param name="hotel">The hotel data to be created.</param>
        /// <returns>The created hotel data.</returns>
        public async Task<Hotel> Create(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        /// <summary>
        /// Deletes a hotel based on its ID.
        /// </summary>
        /// <param name="id">The ID of the hotel to be deleted.</param>
        public async Task Delete(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrieves hotel details based on its ID.
        /// </summary>
        /// <param name="HotelId">The ID of the hotel to retrieve.</param>
        /// <returns>The hotel data.</returns>
        public async Task<HotelDTO> GetHotel(int HotelId)
        {
            var Hotel = await _context.Hotels.Select(
                h => new HotelDTO
                {
                    ID = h.ID,
                    Name = h.Name,
                    City = h.City,
                    State = h.State,
                    Phone = h.Phone,
                    HotelRooms = h.HotelRooms.Select(hr => new HotelRoomDTO
                    {
                        HotelID = hr.HotelID,
                        RoomNumber = hr.RoomNumber,
                        Rate = hr.Rate,
                        PetFriendly = hr.PetFriendly,
                        RoomID = hr.RoomID,
                        Room = new RoomDTO
                        {
                            ID = hr.Room.ID,
                            Name = hr.Room.Name,
                            Layout = (int)hr.Room.Layout,
                            Amenities = hr.Room.RoomAminities
                            .Select(a => new AmenityDTO
                            {
                                ID = a.amenity.ID,
                                Name = a.amenity.Name,
                            }
                            ).ToList()
                        }
                    }).ToList()
                }).FirstOrDefaultAsync(h => h.ID == HotelId);

            return Hotel;
        }


        /// <summary>
        /// Retrieves a list of all hotels.
        /// </summary>
        /// <returns>A list of hotel data.</returns>
        public async Task<List<HotelDTO>> GetHotels()
        {
            var Hotel = await _context.Hotels.Select(
                h => new HotelDTO
                {
                    ID = h.ID,
                    Name = h.Name,
                    City = h.City,
                    State = h.State,
                    Phone = h.Phone,
                    HotelRooms = h.HotelRooms.Select(hr => new HotelRoomDTO
                    {
                        HotelID = hr.HotelID,
                        RoomNumber = hr.RoomNumber,
                        Rate = hr.Rate,
                        PetFriendly = hr.PetFriendly,
                        RoomID = hr.RoomID,
                        Room = new RoomDTO
                        {
                            ID = hr.Room.ID,
                            Name = hr.Room.Name,
                            Layout = (int)hr.Room.Layout,
                            Amenities = hr.Room.RoomAminities
                            .Select(a => new AmenityDTO
                            {
                                ID = a.amenity.ID,
                                Name = a.amenity.Name,
                            }
                            ).ToList()
                        }
                    }).ToList()
                }).ToListAsync();
            return Hotel;
        }


        /// <summary>
        /// Updates the details of a hotel.
        /// </summary>
        /// <param name="id">The ID of the hotel to update.</param>
        /// <param name="UpdatedHotel">The updated hotel data.</param>
        /// <returns>The updated hotel data.</returns>
        public async Task<Hotel> UpdateHotel(int id, Hotel UpdatedHotel)
        {
            Hotel CurrentHotel = await _context.Hotels.FindAsync(id);
            if (CurrentHotel != null)
            {
                CurrentHotel.Name = UpdatedHotel.Name;
                CurrentHotel.StreetAddress = UpdatedHotel.StreetAddress;
                CurrentHotel.City = UpdatedHotel.City;
                CurrentHotel.State = UpdatedHotel.State;
                CurrentHotel.Country = UpdatedHotel.Country;
                CurrentHotel.Phone = UpdatedHotel.Phone;
                _context.Entry(CurrentHotel).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return UpdatedHotel;
        }
    }
}
