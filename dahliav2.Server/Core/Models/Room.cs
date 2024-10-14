using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotelReservationSystem.Core.Models;

namespace HotelReservationSystem.Core.Models;

public class Room
{
    [Key]
    private int RoomId { get; set; }
    [Required]
    private string RoomName { get; set; }
    [Required]
    private bool IsAvailable { get; set; }
    [Required]
    private int RoomTypeId { get; set; }
    
    private List<Booking> bookings = new List<Booking>();
    private List<RoomRate> roomrates = new List<RoomRate>();

}