namespace Async_Inn.Models.Interfaces
{
    /// <summary>
    /// Defines the contract for the HotelRoom service.
    /// </summary>
    public interface IHotelRoom
    {
        /// <summary>
        /// Creates a hotel room.
        /// </summary>
        /// <param name="hotelroom">The hotel room to create.</param>
        /// <param name="HotelID">The ID of the hotel to which the room belongs.</param>
        /// <returns>A task that represents the asynchronous operation of creating a hotel room.</returns>
        Task<HotelRoomDTO> Create(HotelRoomDTO hotelroom, int HotelID);

        /// <summary>
        /// Retrieves all hotel rooms for a given hotel.
        /// </summary>
        /// <param name="HotelID">The ID of the hotel for which to retrieve rooms.</param>
        /// <returns>A task that represents the asynchronous operation of retrieving all hotel rooms for a given hotel.</returns>
        Task<List<HotelRoomDTO>> GetHotelRooms(int HotelID);

        /// <summary>
        /// Retrieves a hotel room by its hotel ID and room number.
        /// </summary>
        /// <param name="HotelID">The ID of the hotel to which the room belongs.</param>
        /// <param name="RoomNumber">The room number of the hotel room to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation of retrieving a hotel room by its hotel ID and room number.</returns>
        Task<HotelRoomDTO> GetHotelRoom(int HotelID, int RoomNumber);

        /// <summary>
        /// Updates a hotel room.
        /// </summary>
        /// <param name="HotelID">The ID of the hotel to which the room belongs.</param>
        /// <param name="RoomNumber">The room number of the hotel room to update.</param>
        /// <param name="hotelroom">The updated hotel room information.</param>
        /// <returns>A task that represents the asynchronous operation of updating a hotel room.</returns>
        Task<HotelRoomDTO> UpdateHotelRoom(int HotelID, int RoomNumber, HotelRoomDTO hotelroom);

        /// <summary>
        /// Deletes a hotel room.
        /// </summary>
        /// <param name="HotelID">The ID of the hotel to which the room belongs.</param>
        /// <param name="RoomNumber">The room number of the hotel room to delete.</param>
        /// <returns>A task that represents the asynchronous operation of deleting a hotel room.</returns>
        Task Delete(int HotelID, int RoomNumber);
    }
}
