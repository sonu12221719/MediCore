using System;
using System.ComponentModel.DataAnnotations;
using LabServiceLibrary.Enums;

namespace LabServiceLibrary.Entities;

public class LabTest
{
    [Key]
    public Guid TestID { get; set; }
    public string PatientID { get; set; } = string.Empty;       // logical ref → Patient Service
    public string DoctorID { get; set; } = string.Empty;        // logical ref → Identity Service
    public string? TechnicianID { get; set; }                   // null until admin assigns
    public LabTestType Type { get; set; }
    public LabTestStatus Status { get; set; } = LabTestStatus.Requested;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public LabReport? Report { get; set; }
}
