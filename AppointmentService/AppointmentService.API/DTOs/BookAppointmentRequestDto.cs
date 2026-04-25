using System;

namespace AppointmentService.API.DTOs;

public class BookAppointmentRequestDto
{
    public string DoctorID { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public string? Notes { get; set; }
}
