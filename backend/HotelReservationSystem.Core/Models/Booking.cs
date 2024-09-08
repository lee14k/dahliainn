namespace DefaultNamespace;

public class Booking
{
    private int BookingId;
    private int CustomerId;
    private int RoomId;
    private DateTime CheckInDate;
    private DateTime CheckOutDate; 
    private BookingStatus Status;
    private DateTime CreatedAt;
    private int PaymentId;
    private List<Payment> payments = new List<Payment>();
}