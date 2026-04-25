using System;
using BillingLibrary.Data;
using BillingLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillingLibrary.Repository;


public class BillRepository : IBillRepository
{
    private readonly BillingDbContext _context;

    public BillRepository(BillingDbContext context)
    {
        _context = context;
    }

    public async Task<Bill?> GetByIdAsync(Guid billId)
    {
        return await _context.Bills
            .Include(b => b.Payments)
            .Include(b => b.InsuranceClaims)
            .FirstOrDefaultAsync(b => b.BillID == billId);
    }

    public async Task<IEnumerable<Bill>> GetAllAsync()
    {
        return await _context.Bills
            .Include(b => b.Payments)
            .Include(b => b.InsuranceClaims)
            .OrderByDescending(b => b.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Bill>> GetByPatientIdAsync(string patientId)
    {
        return await _context.Bills
            .Include(b => b.Payments)
            .Include(b => b.InsuranceClaims)
            .Where(b => b.PatientID == patientId)
            .OrderByDescending(b => b.Date)
            .ToListAsync();
    }

    public async Task AddAsync(Bill bill)
    {
        await _context.Bills.AddAsync(bill);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Bill bill)
    {
        bill.UpdatedAt = DateTime.UtcNow;
        _context.Bills.Update(bill);
        await _context.SaveChangesAsync();
    }
}
