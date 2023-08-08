namespace Async_Inn.Models.Interfaces
{
    /// <summary>
    /// Defines the contract for the Aminity service.
    /// </summary>
    public interface IAminity
    {
        /// <summary>
        /// Creates an amenity.
        /// </summary>
        /// <param name="amenity">The amenity to create.</param>
        /// <returns>A task that represents the asynchronous operation of creating an amenity.</returns>
        Task<AmenityDTO> Create(AmenityDTO amenity);

        /// <summary>
        /// Retrieves all amenities.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation of retrieving all amenities.</returns>
        Task<List<AmenityDTO>> GetAmenities();

        /// <summary>
        /// Retrieves an amenity by its ID.
        /// </summary>
        /// <param name="amenityId">The ID of the amenity to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation of retrieving an amenity by its ID.</returns>
        Task<AmenityDTO> GetAmenity(int amenityId);

        /// <summary>
        /// Updates an amenity.
        /// </summary>
        /// <param name="id">The ID of the amenity to update.</param>
        /// <param name="amenity">The updated amenity information.</param>
        /// <returns>A task that represents the asynchronous operation of updating an amenity.</returns>
        Task<Amenity> UpdateAmenity(int id, AmenityDTO amenity);

        /// <summary>
        /// Deletes an amenity.
        /// </summary>
        /// <param name="id">The ID of the amenity to delete.</param>
        /// <returns>A task that represents the asynchronous operation of deleting an amenity.</returns>
        Task Delete(int id);
    }
}
