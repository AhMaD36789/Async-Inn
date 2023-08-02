namespace Async_Inn.Models.Interfaces
{
    public interface IAminity
    {
        Task<Amenity> Create(Amenity amenity);

        // GET All
        Task<List<Amenity>> GetAmenities();

        // GET Amenity By Id

        Task<Amenity> GetAmenity(int amenityId);

        // Update
        Task<Amenity> UpdateAmenity(int id, Amenity amenity);

        // Delete 

        Task Delete(int id);
    }
}
