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
using Microsoft.AspNetCore.Authorization;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _room;

        public RoomsController(IRoom room)
        {
            _room = room;
        }

        // GET: api/Rooms
        [Authorize(policy: "read")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            return await _room.GetRooms();

        }

        // GET: api/Rooms/5
        [Authorize(Policy = "read")]
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            return await _room.GetRoom(id);
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, RoomDTO room)
        {
            var updateRoom = await _room.UpdateRoom(id, room);

            return Ok(updateRoom);
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754\
        [Authorize(Policy = "create")]
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(RoomDTO room)
        {
            await _room.Create(room);

            // Rurtn a 201 Header to Browser or the postmane
            return CreatedAtAction("GetRoom", new { id = room.ID }, room);
        }

        // DELETE: api/Rooms/5
        [Authorize(Policy = "delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _room.Delete(id);
            return NoContent();
        }
        [Authorize(Policy = "create")]
        [HttpPost]
        //LINK: api/1/Amenity/1
        [Route("{roomID}/Amenity/{amenityID}")]
        public async Task<ActionResult<Room>> PostAmenityToRoom(int roomID, int amenityID)
        {
            var addedAmenity = await _room.AddAmenityToRoom(roomID, amenityID);
            return Ok(addedAmenity);
        }

        [Authorize(Policy = "delete")]
        [HttpDelete]
        [Route("{roomID}/Amenity/{amenityID}")]
        public async Task<ActionResult<Room>> DeleteAmenityFromRoom(int roomID, int amenityID)
        {
            await _room.RemoveAmenityFromoRoom(roomID, amenityID);
            return NoContent();
        }
    }
}
