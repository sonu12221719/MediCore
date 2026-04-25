using System;
using Microsoft.EntityFrameworkCore;
using PharmacyLibrary.Data;
using PharmacyLibrary.Entities;

namespace PharmacyLibrary.Repository;

public class MedicineRepository:IMedicineRepository
{
    private readonly PharmacyDbContext _context;

    public MedicineRepository(PharmacyDbContext context)
    {
        _context = context;
    }

    public async Task<Medicine?> GetByIdAsync(Guid medicineId)
    {
        return await _context.Medicines
            .FirstOrDefaultAsync(m => m.MedicineID == medicineId);
    }

    public async Task<IEnumerable<Medicine>> GetAllAsync()
    {
        return await _context.Medicines
            .OrderBy(m => m.Name)
            .ToListAsync();
    }

    public async Task AddAsync(Medicine medicine)
    {
        await _context.Medicines.AddAsync(medicine);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Medicine medicine)
    {
        medicine.UpdatedAt = DateTime.UtcNow;
        _context.Medicines.Update(medicine);
        await _context.SaveChangesAsync();
    }
}
