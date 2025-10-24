using System.Collections.Generic;
using System.Threading.Tasks;
using hotel_santa_ursula_II.Data;
using hotel_santa_ursula_II.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hotel_santa_ursula_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RidersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RidersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rider>>> GetRiders()
        {
            var riders = await _context.Riders.ToListAsync();
            return Ok(riders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rider>> GetRider(int id)
        {
            var rider = await _context.Riders.FindAsync(id);
            if (rider == null)
            {
                return NotFound();
            }

            return Ok(rider);
        }

        [HttpPost]
        public async Task<ActionResult<Rider>> PostRider(Rider rider)
        {
            _context.Riders.Add(rider);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRider), new { id = rider.Id }, rider);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRider(int id, Rider rider)
        {
            if (id != rider.Id)
            {
                return BadRequest();
            }

            var existingRider = await _context.Riders.FindAsync(id);
            if (existingRider == null)
            {
                return NotFound();
            }

            existingRider.FullName = rider.FullName;
            existingRider.PhoneNumber = rider.PhoneNumber;
            existingRider.PreferredPaymentMethod = rider.PreferredPaymentMethod;
            existingRider.Email = rider.Email;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRider(int id)
        {
            var rider = await _context.Riders.FindAsync(id);
            if (rider == null)
            {
                return NotFound();
            }

            _context.Riders.Remove(rider);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
