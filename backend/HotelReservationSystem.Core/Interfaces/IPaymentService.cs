using System.Threading.Tasks;
using HotelReservationSystem.Core.Models;
using HotelReservationSystem.Core.Services;

namespace HotelReservationSystem.Core.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponse> ProcessPayment(PaymentRequestModel request);
    }
}