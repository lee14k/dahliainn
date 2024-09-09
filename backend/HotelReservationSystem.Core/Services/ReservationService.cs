using System;
using System.Linq;
using System.Threading.Tasks;
using HotelReservationSystem.Core.Interfaces;
using HotelReservationSystem.Core.Models;
using HotelReservationSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Core.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _dbContext;

        public ReservationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Booking> CreateReservationAsync(Booking request)
        {
            // Validate the request
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Check if the room is available for the requested dates
            if (!await IsRoomAvailable(request.RoomId, request.CheckInDate, request.CheckOutDate))
                throw new InvalidOperationException("The requested room is not available for the specified dates.");

            // Create a new booking
            var booking = new Booking
            {
                CustomerId = request.CustomerId,
                RoomId = request.RoomId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                Status = BookingStatus.Pending
                // CreatedAt is set in the Booking constructor
            };

            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();

            return booking;
        }

        public async Task<Booking> UpdateReservationAsync(Booking request)
        {
            if (request == null || request.BookingId <= 0)
                throw new ArgumentException("Invalid booking details provided.");

            var booking = await _dbContext.Bookings.FindAsync(request.BookingId);
            if (booking == null)
                throw new InvalidOperationException("Booking not found.");

            // Check if the new room (if changed) is available for the new dates
            if (booking.RoomId != request.RoomId || booking.CheckInDate != request.CheckInDate || booking.CheckOutDate != request.CheckOutDate)
            {
                if (!await IsRoomAvailable(request.RoomId, request.CheckInDate, request.CheckOutDate, request.BookingId))
                    throw new InvalidOperationException("The requested room is not available for the specified dates.");
            }

            // Update booking details
            booking.RoomId = request.RoomId;
            booking.CheckInDate = request.CheckInDate;
            booking.CheckOutDate = request.CheckOutDate;
            // You might want to add logic here to determine if the status should change

            await _dbContext.SaveChangesAsync();

            return booking;
        }

        public async Task CancelReservationAsync(int bookingId)
        {
            var booking = await _dbContext.Bookings.FindAsync(bookingId);
            if (booking == null)
                throw new InvalidOperationException("Booking not found.");

            booking.Status = BookingStatus.Cancelled;
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate, int? excludeBookingId = null)
        {
            var conflictingBookings = await _dbContext.Bookings
                .Where(b => b.RoomId == roomId &&
                            b.Status != BookingStatus.Cancelled &&
                            b.CheckInDate < checkOutDate &&
                            b.CheckOutDate > checkInDate &&
                            (excludeBookingId == null || b.BookingId != excludeBookingId))
                .AnyAsync();

            return !conflictingBookings;
        }
    }
}