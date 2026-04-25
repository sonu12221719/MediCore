using System;
using System.ComponentModel.DataAnnotations;
using ComplianceLibrary.Enums;

namespace ComplianceLibrary.Entities;

public class Audit
{
    [Key]
    public Guid AuditID { get; set; }

    [Required]
    [MaxLength(100)]
    public string AdminID { get; set; } = string.Empty;        // logical ref → Identity Service

    [Required]
    [MaxLength(300)]
    public string Scope { get; set; } = string.Empty;          // e.g. "Patient Records Q1 2024"

    [MaxLength(2000)]
    public string? Findings { get; set; }

    [Required]
    public AuditStatus Status { get; set; } = AuditStatus.Scheduled;

    public DateTime Date { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
