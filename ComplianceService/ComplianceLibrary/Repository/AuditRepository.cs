using System;
using ComplianceLibrary.Data;
using ComplianceLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComplianceLibrary.Repository;

public class AuditRepository:IAuditRepository
{
    private readonly ComplianceDbContext _context;

    public AuditRepository(ComplianceDbContext context)
    {
        _context = context;
    }

    public async Task<Audit?> GetByIdAsync(Guid auditId)
        => await _context.Audits
            .FirstOrDefaultAsync(a => a.AuditID == auditId);

    public async Task<IEnumerable<Audit>> GetAllAsync()
        => await _context.Audits
            .OrderByDescending(a => a.Date)
            .ToListAsync();

    public async Task AddAsync(Audit audit)
    {
        await _context.Audits.AddAsync(audit);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Audit audit)
    {
        audit.UpdatedAt = DateTime.UtcNow;
        _context.Audits.Update(audit);
        await _context.SaveChangesAsync();
    }
}
