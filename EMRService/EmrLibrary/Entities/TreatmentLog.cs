using System;
using System.ComponentModel.DataAnnotations;
using EmrLibrary.Enums;

namespace EmrLibrary.Entities;

public class TreatmentLog
{
    [Key]
    public Guid LogID { get; set; }
    public Guid EMRID { get; set; }                            // FK → EMR
    public string NurseID { get; set; } = string.Empty;       // logical ref → Identity Service
    public string Notes { get; set; } = string.Empty;
    public TreatmentLogStatus Status { get; set; } = TreatmentLogStatus.Recorded;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public EMR EMR { get; set; } = null!;
}
