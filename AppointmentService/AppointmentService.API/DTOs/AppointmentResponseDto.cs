using System;

namespace AppointmentService.API.DTOs;

public class AppointmentResponseDto
{
    public Guid AppointmentID { get; set; }
    public string PatientID { get; set; } = string.Empty;
    public string DoctorID { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
