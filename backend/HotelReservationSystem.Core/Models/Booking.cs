using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DefaultNamespace;
using HotelReservationSystem.Core.Models;

namespace HotelReservationSystem.Core.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        [Required]
        public int CustomerId { get; set; }        
        [Required]
        public int RoomId { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        public BookingStatus Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        [ForeignKey("PaymentId")]
        public int? PaymentId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
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