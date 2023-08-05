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

        //POST: api/Hotels/{HotelID}/{Rooms}

        [HttpPost("/api/Hotels/{HotelID}/Rooms")]
        public async Task<ActionResult> Create(HotelRoomDTO hotelroom, int HotelID)
        {
            var addedHotelRoom = await _context.Create(hotelroom, HotelID);
            return Ok(addedHotelRoom);

        }

        //DELETE: api/Hotels/{HotelID}/Rooms/{RoomNumber}

        [HttpDelete("{HotelID}/Rooms/{RoomNumber}")]
        public async Task<ActionResult> Delete(int HotelID, int RoomNumber)
        {
            _context.Delete(HotelID, RoomNumber);
            return NoContent();
        }

        //GET: api/Hotels/{HotelID}/Rooms/{RoomNumber}

        [HttpGet]
        [Route("/api/Hotels/{HotelID}/Rooms/{RoomNumber}")]
        public async Task<HotelRoomDTO> GetHotelRoom(int HotelID, int RoomNumber)
        {
            var HotelRoom = await _context.GetHotelRoom(HotelID, RoomNumber);
            return HotelRoom;
        }

        //GET: api/Hotels/{HotelID}/Rooms

        [HttpGet]
        [Route("/api/Hotels/{HotelID}/Rooms")]
        public async Task<List<HotelRoomDTO>> GetHotelRooms(int HotelID)
        {
            var HotelRooms = await _context.GetHotelRooms(HotelID);
            return HotelRooms;
        }

        //PUT: api/Hotels/{HotelID}/Rooms/{RoomNumber}

        [HttpPut("{HotelID}/Rooms/{RoomNumber}")]
        public async Task<HotelRoomDTO> UpdateHotelRoom(int HotelID, int RoomNumber, HotelRoomDTO hotelroom)
        {
            var currentHotelRoom = await _context.UpdateHotelRoom(HotelID, RoomNumber, hotelroom);
            return hotelroom;
        }
    }
}
