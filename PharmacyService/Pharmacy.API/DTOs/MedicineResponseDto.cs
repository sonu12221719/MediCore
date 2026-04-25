using System;

namespace Pharmacy.API.DTOs;

public class MedicineResponseDto
{
    public Guid MedicineID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Stock { get; set; }
    public DateOnly ExpiryDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
