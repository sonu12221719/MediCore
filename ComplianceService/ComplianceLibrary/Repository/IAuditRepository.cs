using System;
using ComplianceLibrary.Entities;

namespace ComplianceLibrary.Repository;

public interface IAuditRepository
{
    Task<Audit?> GetByIdAsync(Guid auditId);
    Task<IEnumerable<Audit>> GetAllAsync();
    Task AddAsync(Audit audit);
    Task UpdateAsync(Audit audit);
}
