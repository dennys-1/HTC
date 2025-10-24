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
    public class RideRequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RideRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RideRequest>>> GetRideRequests()
        {
            var rides = await _context.RideRequests
                .Include(r => r.Rider)
                .Include(r => r.Driver)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return Ok(rides);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RideRequest>> GetRideRequest(int id)
        {
            var ride = await _context.RideRequests
                .Include(r => r.Rider)
                .Include(r => r.Driver)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (ride == null)
            {
                return NotFound();
            }

            return Ok(ride);
        }

        [HttpPost]
        public async Task<ActionResult<RideRequest>> PostRideRequest(RideRequest rideRequest)
        {
            var riderExists = await _context.Riders.AnyAsync(r => r.Id == rideRequest.RiderId);
            if (!riderExists)
            {
                return BadRequest($"El pasajero con id {rideRequest.RiderId} no existe.");
            }

            rideRequest.Status = RideStatus.Pending;
            rideRequest.CreatedAt = DateTime.UtcNow;
            rideRequest.AcceptedAt = null;
            rideRequest.CompletedAt = null;
            rideRequest.DriverId = null;

            _context.RideRequests.Add(rideRequest);
            await _context.SaveChangesAsync();

            await _context.Entry(rideRequest).Reference(r => r.Rider).LoadAsync();

            return CreatedAtAction(nameof(GetRideRequest), new { id = rideRequest.Id }, rideRequest);
        }

        [HttpPut("{id}/assign/{driverId}")]
        public async Task<IActionResult> AssignDriver(int id, int driverId)
        {
            var ride = await _context.RideRequests
                .Include(r => r.Driver)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (ride == null)
            {
                return NotFound();
            }

            if (ride.Status != RideStatus.Pending)
            {
                return BadRequest("Solo se pueden asignar conductores a viajes pendientes.");
            }

            var driver = await _context.Drivers.FindAsync(driverId);
            if (driver == null)
            {
                return NotFound($"No se encontró el conductor {driverId}.");
            }

            if (!driver.IsAvailable)
            {
                return BadRequest("El conductor no está disponible.");
            }

            ride.DriverId = driverId;
            ride.Status = RideStatus.Accepted;
            ride.AcceptedAt = DateTime.UtcNow;

            driver.IsAvailable = false;
            driver.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, RideStatusUpdate update)
        {
            var ride = await _context.RideRequests
                .Include(r => r.Driver)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (ride == null)
            {
                return NotFound();
            }

            if (!Enum.IsDefined(typeof(RideStatus), update.Status))
            {
                return BadRequest("Estado de viaje no válido.");
            }

            if (update.Status == RideStatus.Accepted && ride.DriverId == null)
            {
                return BadRequest("Debe asignar un conductor antes de aceptar el viaje.");
            }

            ride.Status = update.Status;

            switch (update.Status)
            {
                case RideStatus.Accepted:
                    ride.AcceptedAt ??= DateTime.UtcNow;
                    break;
                case RideStatus.InProgress:
                    ride.AcceptedAt ??= DateTime.UtcNow;
                    break;
                case RideStatus.Completed:
                    ride.CompletedAt = DateTime.UtcNow;
                    if (ride.Driver != null)
                    {
                        ride.Driver.IsAvailable = true;
                        ride.Driver.UpdatedAt = DateTime.UtcNow;
                    }
                    break;
                case RideStatus.Cancelled:
                    ride.CompletedAt = null;
                    if (ride.Driver != null)
                    {
                        ride.Driver.IsAvailable = true;
                        ride.Driver.UpdatedAt = DateTime.UtcNow;
                    }
                    break;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRideRequest(int id)
        {
            var ride = await _context.RideRequests.FindAsync(id);
            if (ride == null)
            {
                return NotFound();
            }

            _context.RideRequests.Remove(ride);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public class RideStatusUpdate
        {
            public RideStatus Status { get; set; }
        }
    }
}
