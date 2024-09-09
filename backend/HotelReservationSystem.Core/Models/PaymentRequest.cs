namespace HotelReservationSystem.Core.Models;

public class PaymentRequest
{
    public int BookingId { get; set; }
    public string SourceId { get; set; }
    public long Amount { get; set; }
    public string Currency { get; set; }
    
}