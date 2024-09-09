using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotelReservationSystem.Core.Models;

namespace HotelReservationSystem.Core.Models;

public class RoomType
{
    [Key]
    private int RoomTypeId { get; set; }
    [Required]
    private int CustomerId { get; set; }
    [Required]
    private decimal DefaultRate { get; set; }
    [Required]
    private int Capacity { get; set; } 
    [Required]
    private string Amenities { get; set; }
    [Required]
    private string Description { get; set; }
    [Required]
    private string Image { get; set; }
    [Required]
    private string BedSize { get; set; }
    private List<Room> rooms = new List<Room>();

}