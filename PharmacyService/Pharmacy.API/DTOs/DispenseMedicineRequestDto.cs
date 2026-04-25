using System;

namespace Pharmacy.API.DTOs;

public class DispenseMedicineRequestDto
{
    public Guid MedicineID { get; set; }
    public string PrescriptionID { get; set; } = string.Empty;
    public int Quantity { get; set; }
}
