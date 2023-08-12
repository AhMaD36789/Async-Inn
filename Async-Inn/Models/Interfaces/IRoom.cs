using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Async_Inn.Models.Interfaces
{
    /// <summary>
    /// Defines the contract for the Room service.
    /// </summary>
    public interface IRoom
    {
        /// <summary>
        /// Creates a room.
        /// </summary>
        /// <param name="room">The room to create.</param>
        /// <returns>A task that represents the asynchronous operation of creating a room.</returns>
        Task<Room> Create(RoomDTO room);

        /// <summary>
        /// Retrieves all rooms.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation of retrieving all rooms.</returns>
        Task<List<RoomDTO>> GetRooms();

        /// <summary>
        /// Retrieves a room by its ID.
        /// </summary>
        /// <param name="roomId">The ID of the room to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation of retrieving a room by its ID.</returns>
        Task<RoomDTO> GetRoom(int roomId);

        /// <summary>
        /// Updates a room.
        /// </summary>
        /// <param name="id">The ID of the room to update.</param>
        /// <param name="room">The updated room information.</param>
        /// <returns>A task that represents the asynchronous operation of updating a room.</returns>
        Task<Room> UpdateRoom(int id, RoomDTO room);

        /// <summary>
        /// Deletes a room.
        /// </summary>
        /// <param name="id">The ID of the room to delete.</param>
        /// <returns>A task that represents the asynchronous operation of deleting a room.</returns>
        Task Delete(int id);

        /// <summary>
        /// Adds an amenity to a room.
        /// </summary>
        /// <param name="roomID">The ID of the room to which to add the amenity.</param>
        /// <param name="amenityID">The ID of the amenity to add to the room.</param>
        /// <returns>A task that represents the asynchronous operation of adding an amenity to a room.</returns>
        Task<RoomAmenity> AddAmenityToRoom(int roomID, int amenityID);

        /// <summary>
        /// Removes an amenity from a room.
        ///</summary>
        ///<param name="roomID">The ID of the room from which to remove the amenity.</param>
        ///<param name="amenityID">The ID of the amenity to remove from the room.</param>
        ///<returns>A task that represents the asynchronous operation of removing an amenity from a room.</returns> 
        Task RemoveAmenityFromoRoom(int roomID, int amenityID);


    }
}
