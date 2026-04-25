using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BillingLibrary.Enums;

namespace BillingLibrary.Entities;

public class Bill
{
    [Key]
    public Guid BillID { get; set; }

    [Required]
    [MaxLength(100)]
    public string PatientID { get; set; } = string.Empty;      // logical ref → Patient Service

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }                         // total bill amount

    [Column(TypeName = "decimal(18,2)")]
    public decimal PaidAmount { get; set; } = 0;               // tracks total paid so far

    [Required]
    public BillStatus Status { get; set; } = BillStatus.Draft;

    [MaxLength(500)]
    public string? Description { get; set; }                   // e.g. "Consultation + Lab Tests"

    public DateTime Date { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public ICollection<InsuranceClaim> InsuranceClaims { get; set; } = new List<InsuranceClaim>();
}
