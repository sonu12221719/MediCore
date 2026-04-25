using System;
using System.ComponentModel.DataAnnotations;
using ComplianceLibrary.Enums;

namespace ComplianceLibrary.Entities;

public class ComplianceRecord
{
    [Key]
    public Guid ComplianceID { get; set; }

    [Required]
    [MaxLength(100)]
    public string PatientID { get; set; } = string.Empty;      // logical ref → Patient Service

    [Required]
    public ComplianceType Type { get; set; }                   // Policy or Document

    [Required]
    public ComplianceResult Result { get; set; } = ComplianceResult.Pending;

    [MaxLength(1000)]
    public string? Notes { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
