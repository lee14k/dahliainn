using System;
using System.Collections.Generic;

namespace HotelReservationSystem.Core.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal SalesTax { get; set; }
        public decimal Fees { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TransactionLink { get; set; }
    }
}