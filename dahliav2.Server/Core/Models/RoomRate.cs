using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Core.Models;

public class RoomRate
{
    [Key]
    private int RoomRateId { get; set; }
    [Required]
    private int RoomId { get; set; }
    [Required]
    private decimal Rate { get; set; }
    [Required]
    private DateTime StartDateForRate { get; set; }
    [Required]
    private DateTime EndDateForRate { get; set; }
}