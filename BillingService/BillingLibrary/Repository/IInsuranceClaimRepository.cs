using System;
using BillingLibrary.Entities;

namespace BillingLibrary.Repository;

public interface IInsuranceClaimRepository
{
    Task<InsuranceClaim?> GetByIdAsync(Guid claimId);
    Task<InsuranceClaim?> GetActiveclaimByBillIdAsync(Guid billId);  // duplicate check
    Task<IEnumerable<InsuranceClaim>> GetByPatientIdAsync(string patientId);
    Task AddAsync(InsuranceClaim claim);
    Task UpdateAsync(InsuranceClaim claim);
}
