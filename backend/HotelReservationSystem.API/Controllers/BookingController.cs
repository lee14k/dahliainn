using HotelReservationSystem.Core.Interfaces;
using HotelReservationSystem.Core.Models;
using HotelReservationSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ApplicationDbContext _dbContext;

        public BookingController(IReservationService reservationService, ApplicationDbContext dbContext)
        {
            _reservationService = reservationService;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] Booking request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var reservation = await _reservationService.CreateReservationAsync(request);
                return CreatedAtAction(nameof(GetBooking), new { id = reservation.BookingId }, reservation);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { error = "An error occurred while processing your request." });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var reservation = await _dbContext.Bookings
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.BookingId == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var reservations = await _dbContext.Bookings
                .Include(r => r.Room)
                .ToListAsync();

            return Ok(reservations);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking request)
        {
            if (id != request.BookingId)
            {
                return BadRequest("ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedReservation = await _reservationService.UpdateReservationAsync(request);
                return Ok(updatedReservation);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { error = "An error occurred while processing your request." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            try
            {
                await _reservationService.CancelReservationAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { error = "An error occurred while processing your request." });
            }
        }
    }
}