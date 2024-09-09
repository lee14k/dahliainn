using System;
using System.Collections.Generic;
using Square;
using Square.Exceptions;
using Square.Models;
using HotelReservationSystem.Core.Interfaces;
using HotelReservationSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HotelReservationSystem.Core.Models;

namespace HotelReservationSystem.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ISquareClient _squareClient;
        private readonly ApplicationDbContext _dbContext;

        public PaymentService(ISquareClient squareClient, ApplicationDbContext dbContext)
        {
            _squareClient = squareClient;
            _dbContext = dbContext;
        }

        public async Task<PaymentResponse> ProcessPayment(PaymentRequest request)
        {
            if (request == null || request.BookingId <= 0)
            {
                throw new ArgumentException("Invalid booking ID");
            }

            try
            {
                // Create the payment request for Square
                var paymentsApi = _squareClient.PaymentsApi;
                var createPaymentRequest = new CreatePaymentRequest(
                    request.SourceId,
                    Guid.NewGuid().ToString(), // Idempotency key for safe retries
                    new Money(request.Amount, request.Currency) // Handle nullable long by providing a default
                );

                // Send the payment request to Square API
                var paymentResponse = await paymentsApi.CreatePaymentAsync(createPaymentRequest);
                var paymentId = paymentResponse.Payment.Id;

                // Find and update the booking in the database
                var booking = await _dbContext.Bookings.FindAsync(request.BookingId);
                if (booking == null)
                {
                    throw new KeyNotFoundException("Booking not found");
                }

                // Update booking with payment details
                booking.PaymentId = paymentId;
                _dbContext.Bookings.Update(booking);
                await _dbContext.SaveChangesAsync();

                // Return custom payment response model
                return new PaymentResponse
                {
                    Id = paymentResponse.Payment.Id,
                    Status = paymentResponse.Payment.Status,
                    Amount = paymentResponse.Payment.AmountMoney.Amount ?? 0,
                    Currency = paymentResponse.Payment.AmountMoney.Currency
                };
            }
            catch (ApiException e)
            {
                // Log or handle the Square API exception properly
                throw new Exception($"Error processing payment with Square API: {e.Message}", e);
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
                throw new Exception("Unexpected error during payment processing", ex);
            }
        }
    }
}