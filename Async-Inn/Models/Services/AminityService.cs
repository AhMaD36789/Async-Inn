using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
    public class AminityService : IAminity
    {
        private readonly AsyncInnDBContext _context;

        public AminityService(AsyncInnDBContext context)
        {
            _context = context;
        }


        public async Task<Aminity> Create(Aminity amenity)
        {
            _context.Amenities.Add(amenity);
            await _context.SaveChangesAsync();
            return amenity;
        }

        public async Task Delete(int id)
        {
            Aminity amenity = await GetAmenity(id);
            if (amenity != null)
            {
                _context.Amenities.Remove(amenity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Aminity>> GetAmenities()
        {
            var Amenities = await _context.Amenities.ToListAsync();
            return Amenities;
        }

        public async Task<Aminity> GetAmenity(int amenityId)
        {
            var Amenity = await _context.Amenities.FindAsync(amenityId);
            return Amenity;
        }

        public async Task<Aminity> UpdateAmenity(int id, Aminity UpdatedAmenity)
        {
            Aminity CurrentAmenity = await GetAmenity(id);

            if (CurrentAmenity != null)
            {
                CurrentAmenity.Name = UpdatedAmenity.Name;
                await _context.SaveChangesAsync();
            }
            return CurrentAmenity;
        }
    }
}
