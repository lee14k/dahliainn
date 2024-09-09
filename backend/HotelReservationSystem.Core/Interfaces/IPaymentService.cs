using System.Threading.Tasks;
using HotelReservationSystem.Core.Models;

namespace HotelReservationSystem.Core.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponse> ProcessPayment(PaymentRequest request);
    }
}