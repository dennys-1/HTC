using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hotel_santa_ursula_II.Data;
using hotel_santa_ursula_II.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hotel_santa_ursula_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DriversController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
        {
            var drivers = await _context.Drivers
                .OrderByDescending(d => d.UpdatedAt)
                .ToListAsync();

            return Ok(drivers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Driver>> GetDriver(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            return Ok(driver);
        }

        [HttpPost]
        public async Task<ActionResult<Driver>> PostDriver(Driver driver)
        {
            driver.UpdatedAt = DateTime.UtcNow;
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDriver), new { id = driver.Id }, driver);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriver(int id, Driver driver)
        {
            if (id != driver.Id)
            {
                return BadRequest();
            }

            var existingDriver = await _context.Drivers.FindAsync(id);
            if (existingDriver == null)
            {
                return NotFound();
            }

            existingDriver.FullName = driver.FullName;
            existingDriver.PhoneNumber = driver.PhoneNumber;
            existingDriver.VehicleModel = driver.VehicleModel;
            existingDriver.LicensePlate = driver.LicensePlate;
            existingDriver.Rating = driver.Rating;
            existingDriver.IsAvailable = driver.IsAvailable;
            existingDriver.CurrentLatitude = driver.CurrentLatitude;
            existingDriver.CurrentLongitude = driver.CurrentLongitude;
            existingDriver.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/availability")]
        public async Task<IActionResult> UpdateAvailability(int id, DriverAvailabilityUpdate update)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }

            driver.IsAvailable = update.IsAvailable;
            driver.CurrentLatitude = update.Latitude ?? driver.CurrentLatitude;
            driver.CurrentLongitude = update.Longitude ?? driver.CurrentLongitude;
            driver.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }

            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public class DriverAvailabilityUpdate
        {
            public bool IsAvailable { get; set; }
            public double? Latitude { get; set; }
            public double? Longitude { get; set; }
        }
    }
}
