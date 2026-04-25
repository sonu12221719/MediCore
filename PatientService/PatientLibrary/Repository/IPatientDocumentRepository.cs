using System;
using PatientLibrary.Entities;

namespace PatientLibrary.Repository;

public interface IPatientDocumentRepository
{
    Task UploadDocumentAsync(PatientDocument document);
    Task<IEnumerable<PatientDocument>> GetAllDocumentAsync();
    Task<PatientDocument?> GetDocumentByIdAsync(Guid documentId);
    Task DeleteDocumentAsync(Guid documentId);
}
