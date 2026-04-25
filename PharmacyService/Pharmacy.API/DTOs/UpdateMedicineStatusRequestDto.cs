using System;
using PharmacyLibrary.Enums;

namespace Pharmacy.API.DTOs;

public class UpdateMedicineStatusRequestDto
{
    public MedicineStatus Status { get; set; }
}
