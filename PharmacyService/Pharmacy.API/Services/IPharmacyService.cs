using System;
using Pharmacy.API.DTOs;

namespace Pharmacy.API.Services;

public interface IPharmacyService
{
    Task<MedicineResponseDto> AddMedicineAsync(AddMedicineRequestDto dto);
    Task<IEnumerable<MedicineResponseDto>> GetAllMedicinesAsync();
    Task<MedicineResponseDto> GetMedicineByIdAsync(Guid medicineId);
    Task<MedicineResponseDto> UpdateStockAsync(Guid medicineId, UpdateStockRequestDto dto);
    Task<MedicineResponseDto> UpdateMedicineStatusAsync(Guid medicineId, UpdateMedicineStatusRequestDto dto);
    Task<DispenseResponseDto> DispenseMedicineAsync(DispenseMedicineRequestDto dto);
    Task<IEnumerable<DispenseResponseDto>> GetAllDispensesAsync();
    Task<DispenseResponseDto> GetDispenseByIdAsync(Guid dispenseId);
    Task<DispenseResponseDto> GetDispenseByPrescriptionAsync(string prescriptionId);
}
