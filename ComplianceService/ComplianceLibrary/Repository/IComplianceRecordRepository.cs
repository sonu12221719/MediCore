using System;
using ComplianceLibrary.Entities;

namespace ComplianceLibrary.Repository;

public interface IComplianceRecordRepository
{
    Task<ComplianceRecord?> GetByIdAsync(Guid complianceId);
    Task<IEnumerable<ComplianceRecord>> GetAllAsync();
    Task<IEnumerable<ComplianceRecord>> GetByPatientIdAsync(string patientId);
    Task AddAsync(ComplianceRecord record);
    Task UpdateAsync(ComplianceRecord record);
}
