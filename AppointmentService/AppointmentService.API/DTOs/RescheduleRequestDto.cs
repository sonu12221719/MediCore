using System;

namespace AppointmentService.API.DTOs;

public class RescheduleRequestDto
{
    public DateOnly NewDate { get; set; }
    public TimeOnly NewTime { get; set; }
}
