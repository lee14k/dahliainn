using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotelReservationSystem.Core.Models;

namespace HotelReservationSystem.Core.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
        [Required]
        [MaxLength(100)]
        public string ZipCode { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}