using System;
using LabServiceLibrary.Data;
using LabServiceLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabServiceLibrary.Repository;

public class LabTestRepository:ILabTestRepository
{
    private readonly LabServiceDBContext _context;

    public LabTestRepository(LabServiceDBContext context)
    {
        _context = context;
    }

    public async Task<LabTest?> GetByIdAsync(Guid testId)
    {
        return await _context.LabTests
            .Include(t => t.Report)
            .FirstOrDefaultAsync(t => t.TestID == testId);
    }

    public async Task<IEnumerable<LabTest>> GetAllAsync()
    {
        return await _context.LabTests
            .Include(t => t.Report)
            .OrderByDescending(t => t.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<LabTest>> GetByPatientIdAsync(string patientId)
    {
        return await _context.LabTests
            .Include(t => t.Report)
            .Where(t => t.PatientID == patientId)
            .OrderByDescending(t => t.Date)
            .ToListAsync();
    }

    public async Task AddAsync(LabTest labTest)
    {
        await _context.LabTests.AddAsync(labTest);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(LabTest labTest)
    {
        labTest.UpdatedAt = DateTime.UtcNow;
        _context.LabTests.Update(labTest);
        await _context.SaveChangesAsync();
    }
}
