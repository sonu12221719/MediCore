using System;
using Compliance.API.DTOs;
using Compliance.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Compliance.API.Controllers;

[ApiController]
[Route("api/compliance")]
[Authorize]
public class ComplianceController : ControllerBase
{
    private readonly IComplianceService _complianceService;

    public ComplianceController(IComplianceService complianceService)
    {
        _complianceService = complianceService;
    }

    // POST /api/compliance/records
    [HttpPost("records")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateRecord([FromBody] CreateComplianceRecordRequestDto dto)
    {
        var result = await _complianceService.CreateRecordAsync(dto);
        return CreatedAtAction(nameof(GetRecordById), new { id = result.ComplianceID }, result);
    }

    // GET /api/compliance/records
    [HttpGet("records")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllRecords()
    {
        var result = await _complianceService.GetAllRecordsAsync();
        return Ok(result);
    }

    // GET /api/compliance/records/{id}
    [HttpGet("records/{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetRecordById(Guid id)
    {
        var result = await _complianceService.GetRecordByIdAsync(id);
        return Ok(result);
    }

    // GET /api/compliance/records/patient/{patientId}
    [HttpGet("records/patient/{patientId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetRecordsByPatient(string patientId)
    {
        var result = await _complianceService.GetRecordsByPatientAsync(patientId);
        return Ok(result);
    }

    // PUT /api/compliance/records/{id}/result
    [HttpPut("records/{id:guid}/result")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateRecordResult(Guid id, [FromBody] UpdateComplianceResultRequestDto dto)
    {
        var result = await _complianceService.UpdateRecordResultAsync(id, dto);
        return Ok(result);
    }

    // POST /api/compliance/audits
    [HttpPost("audits")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAudit([FromBody] CreateAuditRequestDto dto)
    {
        var result = await _complianceService.CreateAuditAsync(dto);
        return CreatedAtAction(nameof(GetAuditById), new { id = result.AuditID }, result);
    }

    // GET /api/compliance/audits
    [HttpGet("audits")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAudits()
    {
        var result = await _complianceService.GetAllAuditsAsync();
        return Ok(result);
    }

    // GET /api/compliance/audits/{id}
    [HttpGet("audits/{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAuditById(Guid id)
    {
        var result = await _complianceService.GetAuditByIdAsync(id);
        return Ok(result);
    }

    // PUT /api/compliance/audits/{id}
    [HttpPut("audits/{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAudit(Guid id, [FromBody] UpdateAuditRequestDto dto)
    {
        var result = await _complianceService.UpdateAuditAsync(id, dto);
        return Ok(result);
    }
}
