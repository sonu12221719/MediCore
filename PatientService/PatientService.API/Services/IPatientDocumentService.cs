using System;
using PatientService.API.DTOs;

namespace PatientService.API.Services;

public interface IPatientDocumentService
{
    Task UploadDocumentAsync(string patientId, RequestPatientDocumentDto dto);
    Task<IEnumerable<ResponsePatientDocumentDto>> GetAllDocumentAsync();
    Task<ResponsePatientDocumentDto?> GetDocumentByIdAsync(Guid documentId);
    Task DeleteDocumentAsync(Guid documentId);
}
