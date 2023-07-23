namespace Async_Inn.Models.Interfaces
{
    public interface IAminity
    {
        Task<Aminity> Create(Aminity amenity);

        // GET All
        Task<List<Aminity>> GetAmenities();

        // GET Amenity By Id

        Task<Aminity> GetAmenity(int amenityId);

        // Update
        Task<Aminity> UpdateAmenity(int id, Aminity amenity);

        // Delete 

        Task Delete(int id);
    }
}
