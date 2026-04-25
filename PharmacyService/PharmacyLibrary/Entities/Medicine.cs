using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PharmacyLibrary.Enums;

namespace PharmacyLibrary.Entities;

public class Medicine
{
    [Key]
    public Guid MedicineID { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public MedicineType Type { get; set; }

    [Required]
    public int Stock { get; set; } = 0;

    [Required]
    public DateOnly ExpiryDate { get; set; }

    [Required]
    public MedicineStatus Status { get; set; } = MedicineStatus.Active;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public ICollection<Dispense> Dispenses { get; set; } = new List<Dispense>();
}
