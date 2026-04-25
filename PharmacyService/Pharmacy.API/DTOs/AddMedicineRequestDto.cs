using System;
using PharmacyLibrary.Enums;

namespace Pharmacy.API.DTOs;

public class AddMedicineRequestDto
{
    public string Name { get; set; } = string.Empty;
    public MedicineType Type { get; set; }
    public int Stock { get; set; }
    public DateOnly ExpiryDate { get; set; }
}
