using System.Text.Json;

namespace HotelReservationSystem.Core.Models;

public class PaymentResponse
{
    public string Id { get; set; } // Payment ID
    public string Status { get; set; } // Payment status (e.g., "COMPLETED")
    public long Amount { get; set; } // Amount in cents
    public string Currency { get; set; } // Currency in ISO 4217 format
}