using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Core.Models
{
    public class Payment
    {
        [Key]
        public string PaymentId { get; set; }
        [Required]
        public int BookingId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public decimal SalesTax { get; set; }
        [Required]
        public decimal Fees { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public string TransactionLink { get; set; }
    }
}