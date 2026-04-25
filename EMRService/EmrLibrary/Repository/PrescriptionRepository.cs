using System;
using EmrLibrary.Data;
using EmrLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmrLibrary.Repository;

public class PrescriptionRepository:IPrescriptionRepository
{
    private readonly EmrDBContext _context;

    public PrescriptionRepository(EmrDBContext context)
    {
        _context = context;
    }

    public async Task<Prescription?> GetByIdAsync(Guid prescriptionId)
    {
        return await _context.Prescriptions
            .Include(p => p.EMR)
            .FirstOrDefaultAsync(p => p.PrescriptionID == prescriptionId);
    }

    public async Task<IEnumerable<Prescription>> GetByEMRIdAsync(Guid emrId)
    {
        return await _context.Prescriptions
            .Where(p => p.EMRID == emrId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(Prescription prescription)
    {
        await _context.Prescriptions.AddAsync(prescription);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Prescription prescription)
    {
        prescription.UpdatedAt = DateTime.UtcNow;
        _context.Prescriptions.Update(prescription);
        await _context.SaveChangesAsync();
    }
}
