namespace Async_Inn.Models.Interfaces
{
    public interface IAminity
    {
        Task<AmenityDTO> Create(AmenityDTO amenity);

        // GET All
        Task<List<AmenityDTO>> GetAmenities();

        // GET Amenity By Id

        Task<AmenityDTO> GetAmenity(int amenityId);

        // Update
        Task<Amenity> UpdateAmenity(int id, AmenityDTO amenity);

        // Delete 

        Task Delete(int id);
    }
}
