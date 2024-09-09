using System;
using System.Collections.Generic;
using HotelReservationSystem.Core.Models;

namespace DefaultNamespace;

public class Customer
{
    private int CustomerId { get; set; }
    private string FirstName { get; set; }
    private string  LastName { get; set; }
    private string  Address { get; set; }
    private string  ZipCode { get; set; }
    private string  Email { get; set; }
    private string  PhoneNumber { get; set; }
    private List<Booking> bookings = new List<Booking>();

}