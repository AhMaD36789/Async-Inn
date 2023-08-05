using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Async_Inn.Models.Interfaces
{
    public interface IRoom
    {
        Task<Room> Create(RoomDTO room);

        // GET All
        Task<List<RoomDTO>> GetRooms();

        // GET Room By Id

        Task<RoomDTO> GetRoom(int roomId);

        // Update
        Task<Room> UpdateRoom(int id, RoomDTO room);

        // Delete 

        Task Delete(int id);

        //Add Amenity to Room

        Task<RoomAmenity> AddAmenityToRoom(int roomID, int amenityID);

        //Remove Amenity from Room

        Task RemoveAmenityFromoRoom(int roomID, int amenityID);
    }
}
