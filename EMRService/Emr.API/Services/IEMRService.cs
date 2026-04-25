using System;
using Emr.API.DTOs;

namespace Emr.API.Services;

public interface IEMRService
{
    Task<EMRResponseDto> CreateAsync(CreateEMRRequestDto dto);
    Task<EMRResponseDto> GetByIdAsync(Guid emrId);
    Task<IEnumerable<EMRResponseDto>> GetByPatientIdAsync(string patientId);
    Task<EMRResponseDto> UpdateAsync(Guid emrId, UpdateEMRRequestDto dto);
    Task<PrescriptionResponseDto> AddPrescriptionAsync(Guid emrId, AddPrescriptionRequestDto dto);
    Task<IEnumerable<PrescriptionResponseDto>> GetPrescriptionsAsync(Guid emrId);
    Task<TreatmentLogResponseDto> AddTreatmentLogAsync(Guid emrId, AddTreatmentLogRequestDto dto);
    Task<IEnumerable<TreatmentLogResponseDto>> GetTreatmentLogsAsync(Guid emrId);
}
