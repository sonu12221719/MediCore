using System;

namespace AppointmentService.API.DTOs;

public class CreateScheduleRequestDto
{
    public string DoctorID { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public List<TimeOnly> TimeSlots { get; set; } = new();
}
