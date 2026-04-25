using System;
using LabServiceLibrary.Data;
using LabServiceLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabServiceLibrary.Repository;

public class LabReportRepository:ILabReportRepository
{
    private readonly LabServiceDBContext _context;

    public LabReportRepository(LabServiceDBContext context)
    {
        _context = context;
    }

    public async Task<LabReport?> GetByTestIdAsync(Guid testId)
    {
        return await _context.LabReports
            .FirstOrDefaultAsync(r => r.TestID == testId);
    }

    public async Task AddAsync(LabReport report)
    {
        await _context.LabReports.AddAsync(report);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(LabReport report)
    {
        report.UpdatedAt = DateTime.UtcNow;
        _context.LabReports.Update(report);
        await _context.SaveChangesAsync();
    }

}
