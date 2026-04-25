using System;
using BillingLibrary.Entities;

namespace BillingLibrary.Repository;

public interface IBillRepository
{
    Task<Bill?> GetByIdAsync(Guid billId);
    Task<IEnumerable<Bill>> GetAllAsync();
    Task<IEnumerable<Bill>> GetByPatientIdAsync(string patientId);
    Task AddAsync(Bill bill);
    Task UpdateAsync(Bill bill);
}
