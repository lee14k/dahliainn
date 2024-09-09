using System;
using System.Collections.Generic;
namespace DefaultNamespace;

public class RoomRate
{
    private int RoomRateId { get; set; }
    private int RoomId { get; set; }
    private decimal Rate { get; set; }
    private DateTime StartDateForRate { get; set; }
    private DateTime EndDateForRate { get; set; }
}