using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
    /// <summary>
    /// Provides functionality for managing amenities.
    /// </summary>
    public class AminityService : IAminity
    {
        private readonly AsyncInnDBContext _context;

        public AminityService(AsyncInnDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new amenity.
        /// </summary>
        /// <param name="amenity">The amenity to create.</param>
        /// <returns>A task that represents the asynchronous operation of creating an amenity.</returns>
        public async Task<AmenityDTO> Create(AmenityDTO amenity)
        {
            Amenity newAmenity = new Amenity()
            {
                Name = amenity.Name
            };
            _context.Amenities.Add(newAmenity);
            await _context.SaveChangesAsync();
            amenity.ID = newAmenity.ID;
            return amenity;
        }

        /// <summary>
        /// Deletes an amenity.
        /// </summary>
        /// <param name="id">The ID of the amenity to delete.</param>
        /// <returns>A task that represents the asynchronous operation of deleting an amenity.</returns>
        public async Task Delete(int id)
        {
            AmenityDTO amenity = await GetAmenity(id);
            Amenity newAmenity = new Amenity()
            {
                ID = amenity.ID,
                Name = amenity.Name
            };
            _context.Amenities.Remove(newAmenity);
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Retrieves all amenities.
        ///</summary>
        ///<returns>A task that represents the asynchronous operation of retrieving all amenities.</returns> 
        public async Task<List<AmenityDTO>> GetAmenities()
        {

            return await _context.Amenities.Select(x => new AmenityDTO()
            {
                ID = x.ID,
                Name = x.Name
            }).ToListAsync();

        }

        /// <summary>
        /// Retrieves an amenity by its ID.
        ///</summary>
        ///<param name="amenityId">The ID of the amenity to retrieve.</param>
        ///<returns>A task that represents the asynchronous operation of retrieving an amenity by its ID.</returns> 
        public async Task<AmenityDTO> GetAmenity(int amenityId)
        {

            return await _context.Amenities
                .Select(x => new AmenityDTO()
                {
                    ID = x.ID,
                    Name = x.Name
                }

                ).FirstOrDefaultAsync(x => x.ID == amenityId);
        }

        /// <summary>
        /// Updates an amenity.
        ///</summary>
        ///<param name="id">The ID of the amenity to update.</param>
        ///<param name="UpdatedAmenity">The updated information for the amenity.</param>
        ///<returns>A task that represents the asynchronous operation of updating an amenity.</returns> 
        public async Task<Amenity> UpdateAmenity(int id, AmenityDTO UpdatedAmenity)
        {
            var CurrentAmenity = await GetAmenity(id);
            Amenity newCurrent = new Amenity();
            if (CurrentAmenity != null)
            {
                newCurrent = new Amenity()
                {
                    ID = CurrentAmenity.ID,
                    Name = UpdatedAmenity.Name
                };
                _context.Entry(newCurrent).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            return newCurrent;
        }
    }
}
