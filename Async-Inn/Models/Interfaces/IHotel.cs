namespace Async_Inn.Models.Interfaces
{
    /// <summary>
    /// Defines the contract for the Hotel service.
    /// </summary>
    public interface IHotel
    {
        /// <summary>
        /// Creates a hotel.
        /// </summary>
        /// <param name="hotel">The hotel to create.</param>
        /// <returns>A task that represents the asynchronous operation of creating a hotel.</returns>
        Task<Hotel> Create(Hotel hotel);

        /// <summary>
        /// Retrieves all hotels.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation of retrieving all hotels.</returns>
        Task<List<HotelDTO>> GetHotels();

        /// <summary>
        /// Retrieves a hotel by its ID.
        /// </summary>
        /// <param name="HotelId">The ID of the hotel to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation of retrieving a hotel by its ID.</returns>
        Task<HotelDTO> GetHotel(int HotelId);

        /// <summary>
        /// Updates a hotel.
        /// </summary>
        /// <param name="id">The ID of the hotel to update.</param>
        /// <param name="hotel">The updated hotel information.</param>
        /// <returns>A task that represents the asynchronous operation of updating a hotel.</returns>
        Task<Hotel> UpdateHotel(int id, Hotel hotel);

        /// <summary>
        /// Deletes a hotel.
        /// </summary>
        /// <param name="id">The ID of the hotel to delete.</param>
        /// <returns>A task that represents the asynchronous operation of deleting a hotel.</returns>
        Task Delete(int id);
    }
}
