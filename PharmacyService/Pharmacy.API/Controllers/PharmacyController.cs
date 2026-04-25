using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.API.DTOs;
using Pharmacy.API.Services;
using PharmacyLibrary.Enums;

namespace Pharmacy.API.Controllers
{
    
    [ApiController]
    [Route("api/pharmacy")]
    [Authorize]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyService _pharmacyService;

        public PharmacyController(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
        }

        [HttpPost("medicines")]
        [Authorize(Roles = nameof(RoleOption.Doctor))]
        public async Task<IActionResult> AddMedicine([FromBody] AddMedicineRequestDto dto)
        {
            var result = await _pharmacyService.AddMedicineAsync(dto);
            return CreatedAtAction(nameof(GetMedicineById), new { id = result.MedicineID }, result);
        }

        // GET /api/pharmacy/medicines
        [HttpGet("medicines")]
        [Authorize(Roles = $"{nameof(RoleOption.Admin)},{nameof(RoleOption.Doctor)},{nameof(RoleOption.Pharmacist)}")]
        public async Task<IActionResult> GetAllMedicines()
        {
            var result = await _pharmacyService.GetAllMedicinesAsync();
            return Ok(result);
        }

        // GET /api/pharmacy/medicines/{id}
        [HttpGet("medicines/{id:guid}")]
        [Authorize(Roles = $"{nameof(RoleOption.Admin)},{nameof(RoleOption.Doctor)},{nameof(RoleOption.Pharmacist)}")]
        public async Task<IActionResult> GetMedicineById(Guid id)
        {
            var result = await _pharmacyService.GetMedicineByIdAsync(id);
            return Ok(result);
        }

        // PUT /api/pharmacy/medicines/{id}/stock
        [HttpPut("medicines/{id:guid}/stock")]
        [Authorize(Roles = "Pharmacist")]
        public async Task<IActionResult> UpdateStock(Guid id, [FromBody] UpdateStockRequestDto dto)
        {
            var result = await _pharmacyService.UpdateStockAsync(id, dto);
            return Ok(result);
        }

        // PUT /api/pharmacy/medicines/{id}/status
        [HttpPut("medicines/{id:guid}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMedicineStatus(Guid id, [FromBody] UpdateMedicineStatusRequestDto dto)
        {
            var result = await _pharmacyService.UpdateMedicineStatusAsync(id, dto);
            return Ok(result);
        }

        // POST /api/pharmacy/dispense
        [HttpPost("dispense")]
        [Authorize(Roles = "Pharmacist")]
        public async Task<IActionResult> Dispense([FromBody] DispenseMedicineRequestDto dto)
        {
            var result = await _pharmacyService.DispenseMedicineAsync(dto);
            return CreatedAtAction(nameof(GetDispenseById), new { id = result.DispenseID }, result);
        }

        // GET /api/pharmacy/dispense
        [HttpGet("dispense")]
        [Authorize(Roles = "Admin,Pharmacist")]
        public async Task<IActionResult> GetAllDispenses()
        {
            var result = await _pharmacyService.GetAllDispensesAsync();
            return Ok(result);
        }

        // GET /api/pharmacy/dispense/{id}
        [HttpGet("dispense/{id:guid}")]
        [Authorize(Roles = "Admin,Pharmacist,Doctor")]
        public async Task<IActionResult> GetDispenseById(Guid id)
        {
            var result = await _pharmacyService.GetDispenseByIdAsync(id);
            return Ok(result);
        }

        // GET /api/pharmacy/dispense/prescription/{prescriptionId}
        [HttpGet("dispense/prescription/{prescriptionId}")]
        [Authorize(Roles = "Admin,Pharmacist,Doctor")]
        public async Task<IActionResult> GetDispenseByPrescription(string prescriptionId)
        {
            var result = await _pharmacyService.GetDispenseByPrescriptionAsync(prescriptionId);
            return Ok(result);
        }
    }
}
