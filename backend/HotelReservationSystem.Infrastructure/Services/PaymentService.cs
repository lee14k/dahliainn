using Square;
using Square.Exceptions;
using Square.Models;
using HotelReservationSystem.Core.Interfaces; // Import the interface from Core
using HotelReservationSystem.Infrastructure.Data; // For database context
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HotelReservationSystem.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ISquareClient _squareClient;
        private readonly ApplicationDbContext _dbContext;

        public PaymentService(ISquareClient squareClient, ApplicationDbContext dbContext)
        {
            _squareClient = squareClient;  // Injected Square client
            _dbContext = dbContext;        // Injected DB context
        }

        public async Task<PaymentResponse> ProcessPayment(PaymentRequestModel request)
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
                    new Money
                    {
                        Amount = request.Amount, // Amount in cents
                        Currency = request.Currency // Currency (e.g., "USD")
                    }
                );

                // Send the request to Square and await the response
                var paymentResponse = await paymentsApi.CreatePaymentAsync(createPaymentRequest);

                // Extract the payment ID from the response
                var paymentId = paymentResponse.Payment.Id;

                // Update the booking record in the database with the payment ID
                var booking = await _dbContext.Bookings.FindAsync(request.BookingId);
                if (booking == null)
                {
                    throw new KeyNotFoundException("Booking not found");
                }

                // Update the booking with the payment ID
                booking.PaymentId = paymentId; // Ensure PaymentId is a string in your Booking model
                _dbContext.Bookings.Update(booking);
                await _dbContext.SaveChangesAsync();

                // Return the full Square payment response
                return paymentResponse;
            }
            catch (ApiException e)
            {
                // Handle Square API errors
                throw new PaymentProcessingException("Error processing payment with Square API", e);
            }
            catch (Exception ex)
            {
                // Handle generic errors
                throw new PaymentProcessingException("Unexpected error during payment processing", ex);
            }
        }
    }
}