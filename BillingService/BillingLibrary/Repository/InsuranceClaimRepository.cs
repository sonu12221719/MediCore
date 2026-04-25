using System;
using BillingLibrary.Data;
using BillingLibrary.Entities;
using BillingLibrary.Enums;
using Microsoft.EntityFrameworkCore;

namespace BillingLibrary.Repository;

public class InsuranceClaimRepository : IInsuranceClaimRepository
{
    private readonly BillingDbContext _context;

    public InsuranceClaimRepository(BillingDbContext context)
    {
        _context = context;
    }

    public async Task<InsuranceClaim?> GetByIdAsync(Guid claimId)
    {
        return await _context.InsuranceClaims
            .Include(c => c.Bill)
            .FirstOrDefaultAsync(c => c.ClaimID == claimId);
    }

    // Duplicate check — only Submitted or Approved claims block a new claim
    public async Task<InsuranceClaim?> GetActiveclaimByBillIdAsync(Guid billId)
    {
        return await _context.InsuranceClaims
            .FirstOrDefaultAsync(c =>
                c.BillID == billId &&
                (c.Status == ClaimStatus.Submitted || c.Status == ClaimStatus.Approved));
    }

    public async Task<IEnumerable<InsuranceClaim>> GetByPatientIdAsync(string patientId)
    {
        return await _context.InsuranceClaims
            .Include(c => c.Bill)
            .Where(c => c.PatientID == patientId)
            .OrderByDescending(c => c.Date)
            .ToListAsync();
    }

    public async Task AddAsync(InsuranceClaim claim)
    {
        await _context.InsuranceClaims.AddAsync(claim);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(InsuranceClaim claim)
    {
        claim.UpdatedAt = DateTime.UtcNow;
        _context.InsuranceClaims.Update(claim);
        await _context.SaveChangesAsync();
    }
}
