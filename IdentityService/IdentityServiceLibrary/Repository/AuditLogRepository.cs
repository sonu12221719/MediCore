using System;
using IdentityServiceLibrary.Data;
using IdentityServiceLibrary.Entities;

namespace IdentityServiceLibrary.Repository;

public class AuditLogRepository : IAuditLogRepository
{
    private readonly IdentityDBContext _context;
    public AuditLogRepository(IdentityDBContext context)
    {
        _context=context;
    }
    public async Task LogAsync(AuditLog log)
    {
        await _context.AuditLogs.AddAsync(log);
        await _context.SaveChangesAsync();
    }
}
