using System;

namespace Pharmacy.API.DTOs;

public class DispenseResponseDto
{
    public Guid DispenseID { get; set; }
    public Guid MedicineID { get; set; }
    public string MedicineName { get; set; } = string.Empty;
    public string PrescriptionID { get; set; } = string.Empty;
    public string PharmacistID { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}
