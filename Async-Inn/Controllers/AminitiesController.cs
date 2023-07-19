using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Data;
using Async_Inn.Models;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AminitiesController : ControllerBase
    {
        private readonly AsyncInnDBContext _context;

        public AminitiesController(AsyncInnDBContext context)
        {
            _context = context;
        }

        // GET: api/Aminities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aminity>>> GetAmenities()
        {
          if (_context.Amenities == null)
          {
              return NotFound();
          }
            return await _context.Amenities.ToListAsync();
        }

        // GET: api/Aminities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aminity>> GetAminity(int id)
        {
          if (_context.Amenities == null)
          {
              return NotFound();
          }
            var aminity = await _context.Amenities.FindAsync(id);

            if (aminity == null)
            {
                return NotFound();
            }

            return aminity;
        }

        // PUT: api/Aminities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAminity(int id, Aminity aminity)
        {
            if (id != aminity.Id)
            {
                return BadRequest();
            }

            _context.Entry(aminity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AminityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Aminities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Aminity>> PostAminity(Aminity aminity)
        {
          if (_context.Amenities == null)
          {
              return Problem("Entity set 'AsyncInnDBContext.Amenities'  is null.");
          }
            _context.Amenities.Add(aminity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAminity", new { id = aminity.Id }, aminity);
        }

        // DELETE: api/Aminities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAminity(int id)
        {
            if (_context.Amenities == null)
            {
                return NotFound();
            }
            var aminity = await _context.Amenities.FindAsync(id);
            if (aminity == null)
            {
                return NotFound();
            }

            _context.Amenities.Remove(aminity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AminityExists(int id)
        {
            return (_context.Amenities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
