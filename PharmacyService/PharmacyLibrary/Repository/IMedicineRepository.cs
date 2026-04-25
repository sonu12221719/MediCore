using System;
using PharmacyLibrary.Entities;

namespace PharmacyLibrary.Repository;

public interface IMedicineRepository
{
    Task<Medicine?> GetByIdAsync(Guid medicineId);
    Task<IEnumerable<Medicine>> GetAllAsync();
    Task AddAsync(Medicine medicine);
    Task UpdateAsync(Medicine medicine);
}
