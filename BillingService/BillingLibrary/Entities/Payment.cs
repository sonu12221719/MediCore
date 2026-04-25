using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BillingLibrary.Enums;

namespace BillingLibrary.Entities;

public class Payment
{
    [Key]
    public Guid PaymentID { get; set; }

    [Required]
    public Guid BillID { get; set; }                           // FK → Bill

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Required]
    public PaymentMethod Method { get; set; }

    [Required]
    public PaymentStatus Status { get; set; } = PaymentStatus.Completed;

    [MaxLength(200)]
    public string? TransactionRef { get; set; }                // e.g. UPI transaction ID

    public DateTime Date { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    [ForeignKey(nameof(BillID))]
    public Bill Bill { get; set; } = null!;
}
