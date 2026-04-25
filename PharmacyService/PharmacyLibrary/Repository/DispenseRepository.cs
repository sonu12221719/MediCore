using System;
using Microsoft.EntityFrameworkCore;
using PharmacyLibrary.Data;
using PharmacyLibrary.Entities;
using PharmacyLibrary.Enums;

namespace PharmacyLibrary.Repository;

public class DispenseRepository:IDispenseRepository
{
    private readonly PharmacyDbContext _context;

    public DispenseRepository(PharmacyDbContext context)
    {
        _context = context;
    }

    public async Task<Dispense?> GetByIdAsync(Guid dispenseId)
    {
        return await _context.Dispenses
            .Include(d => d.Medicine)
            .FirstOrDefaultAsync(d => d.DispenseID == dispenseId);
    }

    public async Task<IEnumerable<Dispense>> GetAllAsync()
    {
        return await _context.Dispenses
            .Include(d => d.Medicine)
            .OrderByDescending(d => d.Date)
            .ToListAsync();
    }

    public async Task<Dispense?> GetByPrescriptionIdAsync(string prescriptionId)
    {
        return await _context.Dispenses
            .FirstOrDefaultAsync(d =>
                d.PrescriptionID == prescriptionId &&
                d.Status         == DispenseStatus.Dispensed);  // only active dispense counts
    }

    public async Task AddAsync(Dispense dispense)
    {
        await _context.Dispenses.AddAsync(dispense);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Dispense dispense)
    {
        dispense.UpdatedAt = DateTime.UtcNow;
        _context.Dispenses.Update(dispense);
        await _context.SaveChangesAsync();
    }
}
