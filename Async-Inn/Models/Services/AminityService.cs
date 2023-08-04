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

        public async Task<Amenity> Create(Amenity amenity)
        {
            _context.Amenities.Add(amenity);
            await _context.SaveChangesAsync();
            return amenity;
        }

        public async Task Delete(int id)
        {
            Amenity amenity = await GetAmenity(id);
            if (amenity != null)
            {
                _context.Amenities.Remove(amenity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Amenity>> GetAmenities()
        {
            var Amenities = await _context.Amenities.ToListAsync();
            return Amenities;
        }

        public async Task<Amenity> GetAmenity(int amenityId)
        {
            var Amenity = await _context.Amenities.FindAsync(amenityId);
            return Amenity;
        }

        public async Task<Amenity> UpdateAmenity(int id, Amenity UpdatedAmenity)
        {
            Amenity CurrentAmenity = await GetAmenity(id);

            if (CurrentAmenity != null)
            {
                CurrentAmenity.Name = UpdatedAmenity.Name;
                _context.Entry(CurrentAmenity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return CurrentAmenity;
        }
    }
}
