using System;
using Compliance.API.DTOs;

namespace Compliance.API.Services;

public interface IComplianceService
{
    Task<ComplianceRecordResponseDto> CreateRecordAsync(CreateComplianceRecordRequestDto dto);
    Task<IEnumerable<ComplianceRecordResponseDto>> GetAllRecordsAsync();
    Task<ComplianceRecordResponseDto> GetRecordByIdAsync(Guid complianceId);
    Task<IEnumerable<ComplianceRecordResponseDto>> GetRecordsByPatientAsync(string patientId);
    Task<ComplianceRecordResponseDto> UpdateRecordResultAsync(Guid complianceId, UpdateComplianceResultRequestDto dto);
    Task<AuditResponseDto> CreateAuditAsync(CreateAuditRequestDto dto);
    Task<IEnumerable<AuditResponseDto>> GetAllAuditsAsync();
    Task<AuditResponseDto> GetAuditByIdAsync(Guid auditId);
    Task<AuditResponseDto> UpdateAuditAsync(Guid auditId, UpdateAuditRequestDto dto);
}
