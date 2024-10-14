using HotelReservationSystem.Core.Models;
using System.Threading.Tasks;

namespace HotelReservationSystem.Core.Interfaces
{
    public interface IReservationService
    {
        Task<Booking> CreateReservationAsync(Booking request);
        Task<Booking> UpdateReservationAsync(Booking request);
        Task CancelReservationAsync(int bookingId);
    }
}