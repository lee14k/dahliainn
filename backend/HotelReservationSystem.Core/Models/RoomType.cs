namespace DefaultNamespace;

public class RoomType
{
    private int RoomTypeId { get; set; }
    private int CustomerId { get; set; }
    private decimal DefaultRate { get; set; }
    private int Capacity { get; set; } 
    private string Amenities { get; set; }
    private string Description { get; set; }
    private string Image { get; set; }
    private string BedSize { get; set; }
    private List<Room> rooms = new List<Room>();

}