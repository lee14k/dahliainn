using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelReservationSystem.Core.Interfaces;
using HotelReservationSystem.Core.Models;

namespace HotelReservationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IPaymentService _paymentService;

        public BookingController(IReservationService reservationService, IPaymentService paymentService)
        {
            _reservationService = reservationService;
            _paymentService = paymentService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequest request)
        {
            try
            {
                var reservation = new Reservation
                {
                    FirstName = request.BookingData.FirstName,
                    LastName = request.BookingData.LastName,
                    Email = request.BookingData.Email,
                    PhoneNumber = request.BookingData.PhoneNumber,
                    Country = request.BookingData.Country,
                    State = request.BookingData.State,
                    City = request.BookingData.City,
                    StreetAddress = request.BookingData.StreetAddress,
                    ZipCode = request.BookingData.ZipCode,
                    CheckInDate = request.BookingData.StartDate,
                    CheckOutDate = request.BookingData.EndDate,
                    RoomId = request.BookingData.RoomId,
                    PaymentStatus = "pending",
                    SecondGuest = request.BookingData.SecondGuest != null
                        ? new SecondGuest
                        {
                            FirstName = request.BookingData.SecondGuest.FirstName,
                            LastName = request.BookingData.SecondGuest.LastName,
                            Email = request.BookingData.SecondGuest.Email
                        }
                        : null
                };

                if (request.FoodData.Dinner != null)
                {
                    reservation.Dinner = new DinnerOption
                    {
                        Allergies = request.FoodData.Allergies,
                        Preferences = request.FoodData.Preferences,
                        SpecialOccasion = request.FoodData.SpecialOccasion,
                        Time = request.FoodData.Dinner.Time
                    };
                }

                if (request.FoodData.Charcuterie != null)
                {
                    reservation.Charcuterie = new CharcuterieOption
                    {
                        Allergies = request.FoodData.Allergies,
                        Preferences = request.FoodData.Preferences,
                        SpecialOccasion = request.FoodData.SpecialOccasion,
                        Time = request.FoodData.Charcuterie.Time
                    };
                }

                var createdReservation = await _reservationService.CreateReservationAsync(reservation);

                return Ok(new { BookingId = createdReservation.Id });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing your reservation.");
            }
        }
    }
}