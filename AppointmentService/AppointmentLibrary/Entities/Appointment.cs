using AppointmentLibrary.Enums;

namespace AppointmentLibrary.Entities;

public class Appointment
{
    public Guid AppointmentID { get; set; }
    public string PatientID { get; set; } = string.Empty;     // from Patient Service (JWT token)
    public string DoctorID { get; set; } = string.Empty;      // from Identity Service
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public string? Notes { get; set; }                         // optional reason for visit
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public Schedule Schedule { get; set; } = null!;
}
