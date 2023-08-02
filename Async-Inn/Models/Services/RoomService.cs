﻿using Async_Inn.Data;
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


        public async Task<Room> Create(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task Delete(int id)
        {
            Room room = await GetRoom(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Room> GetRoom(int roomId)
        {
            var Room = await _context.Rooms.FindAsync(roomId);
            return Room;
        }

        public async Task<List<Room>> GetRooms()
        {
            return await _context.Rooms
                .Include(ra => ra.RoomAminities)
                .ThenInclude(amenity => amenity.amenity)
                .ToListAsync();
        }


        public async Task<Room> UpdateRoom(int id, Room UpdatedRoom)
        {
            Room CurrentRoom = await GetRoom(id);

            if (CurrentRoom != null)
            {
                CurrentRoom.Name = UpdatedRoom.Name;
                CurrentRoom.Layout = UpdatedRoom.Layout;
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