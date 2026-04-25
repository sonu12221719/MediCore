using System;
using ComplianceLibrary.Data;
using ComplianceLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComplianceLibrary.Repository;

public class ComplianceRecordRepository : IComplianceRecordRepository
{
    private readonly ComplianceDbContext _context;

    public ComplianceRecordRepository(ComplianceDbContext context)
    {
        _context = context;
    }

    public async Task<ComplianceRecord?> GetByIdAsync(Guid complianceId)
        => await _context.ComplianceRecords
            .FirstOrDefaultAsync(r => r.ComplianceID == complianceId);

    public async Task<IEnumerable<ComplianceRecord>> GetAllAsync()
        => await _context.ComplianceRecords
            .OrderByDescending(r => r.Date)
            .ToListAsync();

    public async Task<IEnumerable<ComplianceRecord>> GetByPatientIdAsync(string patientId)
        => await _context.ComplianceRecords
            .Where(r => r.PatientID == patientId)
            .OrderByDescending(r => r.Date)
            .ToListAsync();

    public async Task AddAsync(ComplianceRecord record)
    {
        await _context.ComplianceRecords.AddAsync(record);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ComplianceRecord record)
    {
        record.UpdatedAt = DateTime.UtcNow;
        _context.ComplianceRecords.Update(record);
        await _context.SaveChangesAsync();
    }
}
