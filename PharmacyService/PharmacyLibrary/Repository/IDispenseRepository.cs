using System;
using PharmacyLibrary.Entities;

namespace PharmacyLibrary.Repository;

public interface IDispenseRepository
{
    Task<Dispense?> GetByIdAsync(Guid dispenseId);
    Task<IEnumerable<Dispense>> GetAllAsync();
    Task<Dispense?> GetByPrescriptionIdAsync(string prescriptionId);   // duplicate check
    Task AddAsync(Dispense dispense);
    Task UpdateAsync(Dispense dispense);
}
