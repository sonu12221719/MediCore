using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BillingLibrary.Enums;

namespace BillingLibrary.Entities;

public class InsuranceClaim
{
    [Key]
    public Guid ClaimID { get; set; }

    [Required]
    public Guid BillID { get; set; }                           // FK → Bill

    [Required]
    [MaxLength(100)]
    public string PatientID { get; set; } = string.Empty;      // logical ref → Patient Service

    [Required]
    [MaxLength(100)]
    public string InsuranceID { get; set; } = string.Empty;    // patient's insurance policy number

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }                        // claimed amount

    [Required]
    public ClaimStatus Status { get; set; } = ClaimStatus.Submitted;

    [MaxLength(500)]
    public string? Notes { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    [ForeignKey(nameof(BillID))]
    public Bill Bill { get; set; } = null!;
}
