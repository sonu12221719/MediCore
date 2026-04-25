using System;
using System.Security.Claims;
using AutoMapper;
using Compliance.API.DTOs;
using ComplianceLibrary.Entities;
using ComplianceLibrary.Enums;
using ComplianceLibrary.Exceptions;
using ComplianceLibrary.Repository;

namespace Compliance.API.Services;

public class ComplianceService:IComplianceService
{
    private readonly IComplianceRecordRepository _recordRepository;
    private readonly IAuditRepository _auditRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ComplianceService(
        IComplianceRecordRepository recordRepository,
        IAuditRepository auditRepository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _recordRepository    = recordRepository;
        _auditRepository     = auditRepository;
        _mapper              = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    private string GetUserIdFromToken()
        => _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    // ── COMPLIANCE RECORD ─────────────────────

    public async Task<ComplianceRecordResponseDto> CreateRecordAsync(CreateComplianceRecordRequestDto dto)
    {
        var record = _mapper.Map<ComplianceRecord>(dto);
        await _recordRepository.AddAsync(record);
        return _mapper.Map<ComplianceRecordResponseDto>(record);
    }

    public async Task<IEnumerable<ComplianceRecordResponseDto>> GetAllRecordsAsync()
    {
        var records = await _recordRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ComplianceRecordResponseDto>>(records);
    }

    public async Task<ComplianceRecordResponseDto> GetRecordByIdAsync(Guid complianceId)
    {
        var record = await _recordRepository.GetByIdAsync(complianceId)
            ?? throw new ComplianceRecordNotFoundException(complianceId);

        return _mapper.Map<ComplianceRecordResponseDto>(record);
    }

    public async Task<IEnumerable<ComplianceRecordResponseDto>> GetRecordsByPatientAsync(string patientId)
    {
        var records = await _recordRepository.GetByPatientIdAsync(patientId);
        return _mapper.Map<IEnumerable<ComplianceRecordResponseDto>>(records);
    }

    public async Task<ComplianceRecordResponseDto> UpdateRecordResultAsync(Guid complianceId, UpdateComplianceResultRequestDto dto)
    {
        var record = await _recordRepository.GetByIdAsync(complianceId)
            ?? throw new ComplianceRecordNotFoundException(complianceId);

        record.Result = dto.Result;
        record.Notes  = dto.Notes ?? record.Notes;

        await _recordRepository.UpdateAsync(record);
        return _mapper.Map<ComplianceRecordResponseDto>(record);
    }

    // ── AUDIT ─────────────────────────────────

    public async Task<AuditResponseDto> CreateAuditAsync(CreateAuditRequestDto dto)
    {
        var audit     = _mapper.Map<Audit>(dto);
        audit.AdminID = GetUserIdFromToken();

        await _auditRepository.AddAsync(audit);
        return _mapper.Map<AuditResponseDto>(audit);
    }

    public async Task<IEnumerable<AuditResponseDto>> GetAllAuditsAsync()
    {
        var audits = await _auditRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AuditResponseDto>>(audits);
    }

    public async Task<AuditResponseDto> GetAuditByIdAsync(Guid auditId)
    {
        var audit = await _auditRepository.GetByIdAsync(auditId)
            ?? throw new AuditNotFoundException(auditId);

        return _mapper.Map<AuditResponseDto>(audit);
    }

    public async Task<AuditResponseDto> UpdateAuditAsync(Guid auditId, UpdateAuditRequestDto dto)
    {
        var audit = await _auditRepository.GetByIdAsync(auditId)
            ?? throw new AuditNotFoundException(auditId);

        if (audit.Status == AuditStatus.Completed)
            throw new AuditAlreadyCompletedException(auditId);

        audit.Status   = dto.Status;
        audit.Findings = dto.Findings ?? audit.Findings;

        await _auditRepository.UpdateAsync(audit);
        return _mapper.Map<AuditResponseDto>(audit);
    }
}
