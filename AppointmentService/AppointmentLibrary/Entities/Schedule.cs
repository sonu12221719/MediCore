using System;
using AppointmentLibrary.Enums;

namespace AppointmentLibrary.Entities;

public class Schedule
{
    public Guid ScheduleID { get; set; }
    public string DoctorID { get; set; } = string.Empty;      // logical ref to Identity Service
    public DateOnly Date { get; set; }
    public TimeOnly TimeSlot { get; set; }
    public AvailabilityStatus Availability { get; set; } = AvailabilityStatus.Available;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
