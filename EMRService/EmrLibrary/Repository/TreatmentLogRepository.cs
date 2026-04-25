using System;
using EmrLibrary.Data;
using EmrLibrary.Entities;
using EmrLibrary.Enums;
using Microsoft.EntityFrameworkCore;

namespace EmrLibrary.Repository;

public class TreatmentLogRepository:ITreatmentLogRepository
{
    private readonly EmrDBContext _context;

    public TreatmentLogRepository(EmrDBContext context)
    {
        _context = context;
    }

    public async Task<TreatmentLog?> GetByIdAsync(Guid logId)
    {
        return await _context.TreatmentLogs
            .Include(l => l.EMR)
            .FirstOrDefaultAsync(l => l.LogID == logId);
    }

    public async Task<IEnumerable<TreatmentLog>> GetByEMRIdAsync(Guid emrId)
    {
        return await _context.TreatmentLogs
            .Where(l => l.EMRID == emrId)
            .OrderByDescending(l => l.Date)
            .ToListAsync();
    }

    public async Task AddAsync(TreatmentLog log)
    {
        await _context.TreatmentLogs.AddAsync(log);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TreatmentLog log)
    {
        log.UpdatedAt = DateTime.UtcNow;
        log.Status    = TreatmentLogStatus.Updated;
        _context.TreatmentLogs.Update(log);
        await _context.SaveChangesAsync();
    }
}
