using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using HotelReservationSystem.Core.Models;

namespace HotelReservationSystem.Core.Models
{
    public class Booking
    {
        private int BookingId { get; set; }
        private int CustomerId { get; set; }
        private int RoomId { get; set; }
        private DateTime CheckInDate { get; set; }
        private DateTime CheckOutDate { get; set; }
        private BookingStatus Status { get; set; }
        private DateTime CreatedAt { get; set; }
        private int PaymentId { get; set; }
        private List<Payment> payments = new List<Payment>();
        public List<Payment> Payments => payments;

        public void AddPayment(Payment payment)
        {
            payments.Add(payment);
        }

        public decimal TotalPayments()
        {
            return payments.Sum(p => (decimal)p.Amount);
        }
    }
}