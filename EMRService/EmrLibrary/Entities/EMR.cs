using System;
using System.ComponentModel.DataAnnotations;
using EmrLibrary.Enums;

namespace EmrLibrary.Entities;

public class EMR
{
    [Key]
    public Guid EMRID { get; set; }
    public string PatientID { get; set; } = string.Empty;      // logical ref → Patient Service
    public string DoctorID { get; set; } = string.Empty;       // logical ref → Identity Service
    public string Diagnosis { get; set; } = string.Empty;
    public string TreatmentPlan { get; set; } = string.Empty;
    public EMRStatus Status { get; set; } = EMRStatus.Active;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    public ICollection<TreatmentLog> TreatmentLogs { get; set; } = new List<TreatmentLog>();
}
