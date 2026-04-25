using System;
using System.Security.Claims;
using AutoMapper;
using Pharmacy.API.DTOs;
using PharmacyLibrary.Entities;
using PharmacyLibrary.Enums;
using PharmacyLibrary.Exceptions;
using PharmacyLibrary.Repository;

namespace Pharmacy.API.Services;

public class PharmacyService:IPharmacyService
{
    private readonly IMedicineRepository _medicineRepository;
    private readonly IDispenseRepository _dispenseRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PharmacyService(
        IMedicineRepository medicineRepository,
        IDispenseRepository dispenseRepository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _medicineRepository = medicineRepository;
        _dispenseRepository = dispenseRepository;
        _mapper             = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    private string GetUserIdFromToken()
        => _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    // ADD MEDICINE — Admin only
    public async Task<MedicineResponseDto> AddMedicineAsync(AddMedicineRequestDto dto)
    {
        var medicine = _mapper.Map<Medicine>(dto);
        await _medicineRepository.AddAsync(medicine);
        return _mapper.Map<MedicineResponseDto>(medicine);
    }

    // GET ALL MEDICINES
    public async Task<IEnumerable<MedicineResponseDto>> GetAllMedicinesAsync()
    {
        var medicines = await _medicineRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<MedicineResponseDto>>(medicines);
    }

    // GET MEDICINE BY ID
    public async Task<MedicineResponseDto> GetMedicineByIdAsync(Guid medicineId)
    {
        var medicine = await _medicineRepository.GetByIdAsync(medicineId)
            ?? throw new MedicineNotFoundException(medicineId);

        return _mapper.Map<MedicineResponseDto>(medicine);
    }

    // UPDATE STOCK — Pharmacist only
    public async Task<MedicineResponseDto> UpdateStockAsync(Guid medicineId, UpdateStockRequestDto dto)
    {
        var medicine = await _medicineRepository.GetByIdAsync(medicineId)
            ?? throw new MedicineNotFoundException(medicineId);

        if (dto.Quantity <= 0)
            throw new PharmacyServiceException("Stock quantity must be greater than zero.");

        medicine.Stock += dto.Quantity;

        // Auto set back to Active if it was OutOfStock
        if (medicine.Status == MedicineStatus.OutOfStock)
            medicine.Status = MedicineStatus.Active;

        await _medicineRepository.UpdateAsync(medicine);
        return _mapper.Map<MedicineResponseDto>(medicine);
    }

    // UPDATE STATUS — Admin only
    public async Task<MedicineResponseDto> UpdateMedicineStatusAsync(Guid medicineId, UpdateMedicineStatusRequestDto dto)
    {
        var medicine = await _medicineRepository.GetByIdAsync(medicineId)
            ?? throw new MedicineNotFoundException(medicineId);

        medicine.Status = dto.Status;
        await _medicineRepository.UpdateAsync(medicine);
        return _mapper.Map<MedicineResponseDto>(medicine);
    }

    // DISPENSE MEDICINE — Pharmacist only
    public async Task<DispenseResponseDto> DispenseMedicineAsync(DispenseMedicineRequestDto dto)
    {
        // 1. Get medicine
        var medicine = await _medicineRepository.GetByIdAsync(dto.MedicineID)
            ?? throw new MedicineNotFoundException(dto.MedicineID);

        // 2. Check medicine is active
        if (medicine.Status == MedicineStatus.Inactive)
            throw new PharmacyServiceException($"{medicine.Name} is inactive and cannot be dispensed.");

        // 3. Check expiry
        if (medicine.ExpiryDate < DateOnly.FromDateTime(DateTime.UtcNow))
            throw new MedicineExpiredException(medicine.Name);

        // 4. Check stock
        if (medicine.Stock < dto.Quantity)
            throw new OutOfStockException(medicine.Name, medicine.Stock);

        // 5. Check duplicate dispense
        var existing = await _dispenseRepository.GetByPrescriptionIdAsync(dto.PrescriptionID);
        if (existing is not null)
            throw new DuplicateDispenseException(dto.PrescriptionID);

        // 6. Create dispense record
        var dispense = new Dispense
        {
            DispenseID     = Guid.NewGuid(),
            MedicineID     = dto.MedicineID,
            PrescriptionID = dto.PrescriptionID,
            PharmacistID   = GetUserIdFromToken(),
            Quantity       = dto.Quantity,
            Status         = DispenseStatus.Dispensed,
            Date           = DateTime.UtcNow
        };

        await _dispenseRepository.AddAsync(dispense);

        // 7. Deduct stock
        medicine.Stock -= dto.Quantity;

        // 8. Auto set OutOfStock if stock reaches zero
        if (medicine.Stock == 0)
            medicine.Status = MedicineStatus.OutOfStock;

        await _medicineRepository.UpdateAsync(medicine);

        // Reload dispense with medicine navigation for mapping
        dispense.Medicine = medicine;
        return _mapper.Map<DispenseResponseDto>(dispense);
    }

    // GET ALL DISPENSES
    public async Task<IEnumerable<DispenseResponseDto>> GetAllDispensesAsync()
    {
        var dispenses = await _dispenseRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<DispenseResponseDto>>(dispenses);
    }

    // GET DISPENSE BY ID
    public async Task<DispenseResponseDto> GetDispenseByIdAsync(Guid dispenseId)
    {
        var dispense = await _dispenseRepository.GetByIdAsync(dispenseId)
            ?? throw new DispenseNotFoundException(dispenseId);

        return _mapper.Map<DispenseResponseDto>(dispense);
    }

    // GET DISPENSE BY PRESCRIPTION
    public async Task<DispenseResponseDto> GetDispenseByPrescriptionAsync(string prescriptionId)
    {
        var dispense = await _dispenseRepository.GetByPrescriptionIdAsync(prescriptionId)
            ?? throw new PharmacyServiceException($"No dispense record found for prescription {prescriptionId}.", 404);

        return _mapper.Map<DispenseResponseDto>(dispense);
    }
}
