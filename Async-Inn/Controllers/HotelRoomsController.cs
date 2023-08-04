using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.Interfaces;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _context;

        public HotelRoomsController(IHotelRoom context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("api/Hotels/{HotelID}/Rooms")]
        public async Task<HotelRoom> Create(HotelRoom hotelroom, int HotelID)
        {
            return await _context.Create(hotelroom, HotelID);
        }
        [HttpDelete]
        [Route("{HotelID}/Rooms/{RoomNumber}")]
        public async Task Delete(int HotelID, int RoomNumber)
        {
            await _context.Delete(HotelID, RoomNumber);
        }
        [HttpGet]
        [Route("{HotelID}/Rooms/{RoomNumber}")]
        public async Task<HotelRoom> GetHotelRoom(int HotelID, int RoomNumber)
        {
            return await _context.GetHotelRoom(HotelID, RoomNumber);
        }

        [HttpGet]
        [Route("{HotelID}/Rooms")]
        public async Task<List<HotelRoom>> GetHotelRooms(int HotelID)
        {
            return await _context.GetHotelRooms(HotelID);
        }

        [HttpPut]
        [Route("{HotelID}/Rooms/{RoomNumber}")]
        public async Task<HotelRoom> UpdateHotelRoom(int HotelID, int RoomNumber, HotelRoom hotelroom)
        {

            return await _context.UpdateHotelRoom(HotelID, RoomNumber, hotelroom);
        }
    }
}
