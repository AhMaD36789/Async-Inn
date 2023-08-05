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

        public async Task<List<AmenityDTO>> GetAmenities()
        {

            return await _context.Amenities.Select(x => new AmenityDTO()
            {
                ID = x.ID,
                Name = x.Name
            }).ToListAsync();

        }

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
