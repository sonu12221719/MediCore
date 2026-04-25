using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PharmacyLibrary.Enums;

namespace PharmacyLibrary.Entities;

public class Dispense
{
    [Key]
    public Guid DispenseID { get; set; }

    [Required]
    public Guid MedicineID { get; set; }            // FK → Medicine

    [Required]
    [MaxLength(100)]
    public string PrescriptionID { get; set; } = string.Empty;  // logical ref → EMR Service

    [Required]
    [MaxLength(100)]
    public string PharmacistID { get; set; } = string.Empty;    // logical ref → Identity Service

    [Required]
    public int Quantity { get; set; }

    [Required]
    public DispenseStatus Status { get; set; } = DispenseStatus.Dispensed;

    public DateTime Date { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    [ForeignKey(nameof(MedicineID))]
    public Medicine Medicine { get; set; } = null!;
}
