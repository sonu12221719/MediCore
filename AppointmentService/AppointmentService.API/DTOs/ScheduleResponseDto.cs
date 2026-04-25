using System;

namespace AppointmentService.API.DTOs;

public class ScheduleResponseDto
{
    public Guid ScheduleID { get; set; }
    public string DoctorID { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly TimeSlot { get; set; }
    public string Availability { get; set; } = string.Empty;
}
