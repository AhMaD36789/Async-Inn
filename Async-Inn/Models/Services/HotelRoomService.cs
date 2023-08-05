using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Async_Inn.Models.Services
{
    public class HotelRoomService : IHotelRoom
    {
        private readonly AsyncInnDBContext _context;
        private readonly IRoom _room;


        public HotelRoomService(AsyncInnDBContext context, IRoom room)
        {
            _context = context;
            _room = room;
        }

        public async Task<HotelRoomDTO> Create(HotelRoomDTO hotelroom, int HotelID)
        {
            //Lab 14
            //var room = await _context.Rooms.FindAsync(hotelroom.RoomID);
            //var hotel = await _context.Hotels.FindAsync(hotelroom.HotelID);

            //hotelroom.HotelID = HotelID;

            //hotelroom.Room = room;
            //hotelroom.Hotel = hotel;

            //_context.HotelRooms.Add(hotelroom);
            //await _context.SaveChangesAsync();
            //return hotelroom;

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