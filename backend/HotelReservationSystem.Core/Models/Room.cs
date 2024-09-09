namespace DefaultNamespace;

public class Room
{
    private int RoomId { get; set; }
private string RoomNumber { get; set; }
private int RoomTypeId { get; set; }
    private bool IsAvailable { get; set; }
private List<Booking> bookings = new List<Booking>();
private List<RoomRate> roomrates = new List<RoomRate>();

}