using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Data;
using Async_Inn.Models;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly AsyncInnDBContext _context;

        public HotelRoomsController(AsyncInnDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("api/Hotels/{HotelID}/Rooms")]
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
        [HttpDelete]
        [Route("{HotelID}/Rooms/{RoomNumber}")]
        public async Task Delete(int HotelID, int RoomNumber)
        {
            HotelRoom hotelroom = await GetHotelRoom(HotelID, RoomNumber);
            if (hotelroom != null)
            {
                _context.HotelRooms.Remove(hotelroom);
                await _context.SaveChangesAsync();
            }
        }
        [HttpGet]
        [Route("{HotelID}/Rooms/{RoomNumber}")]
        public async Task<HotelRoom> GetHotelRoom(int HotelID, int RoomNumber)
        {
            var HotelRoom = await _context.HotelRooms.FindAsync(HotelID, RoomNumber);
            return HotelRoom;
        }

        [HttpGet]
        [Route("{HotelID}/Rooms")]
        public async Task<List<HotelRoom>> GetHotelRooms()
        {
            var HotelRooms = await _context.HotelRooms.Include(r => r.Hotel).Include(h => h.Room).ToListAsync();
            return HotelRooms;
        }
        [HttpPut]
        [Route("{HotelID}/Rooms/{RoomNumber}")]
        public async Task<HotelRoom> UpdateHotelRoom(int HotelID, int RoomNumber, HotelRoom hotelroom)
        {
            HotelRoom CurrentHotelRoom = await GetHotelRoom(HotelID, RoomNumber);


            if (CurrentHotelRoom != null)
            {

                CurrentHotelRoom.RoomNumber = hotelroom.RoomNumber;
                CurrentHotelRoom.RoomID = hotelroom.RoomID;
                CurrentHotelRoom.Rate = hotelroom.Rate;
                CurrentHotelRoom.PetFriendly = hotelroom.PetFriendly;
                await _context.SaveChangesAsync();

            }
            return CurrentHotelRoom;
        }
    }
}
