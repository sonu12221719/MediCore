using System;
using System.ComponentModel.DataAnnotations;
using EmrLibrary.Enums;

namespace EmrLibrary.Entities;

public class Prescription
{
    [Key]
    public Guid PrescriptionID { get; set; }
    public Guid EMRID { get; set; }                            // FK → EMR
    public string DoctorID { get; set; } = string.Empty;      // logical ref → Identity Service
    public string Medicine { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;         // e.g. "500mg twice a day"
    public string Duration { get; set; } = string.Empty;       // e.g. "7 days"
    public PrescriptionStatus Status { get; set; } = PrescriptionStatus.Issued;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public EMR EMR { get; set; } = null!;
}
