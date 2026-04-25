using System;
using LabService.API.DTOs;

namespace LabService.API.Services;

public interface ILabService
{
    Task<LabTestResponseDto> CreateTestAsync(CreateLabTestRequestDto dto);
    Task<IEnumerable<LabTestResponseDto>> GetAllTestsAsync();
    Task<LabTestResponseDto> GetTestByIdAsync(Guid testId);
    Task<IEnumerable<LabTestResponseDto>> GetTestsByPatientAsync(string patientId);
    Task<LabTestResponseDto> AssignTechnicianAsync(Guid testId, AssignTechnicianRequestDto dto);
    Task<LabTestResponseDto> UpdateTestStatusAsync(Guid testId, UpdateLabTestStatusRequestDto dto);
    Task<LabReportResponseDto> UploadReportAsync(Guid testId, UploadLabReportRequestDto dto);
    Task<LabReportResponseDto> GetReportAsync(Guid testId);
}
