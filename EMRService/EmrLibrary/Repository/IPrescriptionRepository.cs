using System;
using EmrLibrary.Entities;

namespace EmrLibrary.Repository;

public interface IPrescriptionRepository
{
    Task<Prescription?> GetByIdAsync(Guid prescriptionId);
    Task<IEnumerable<Prescription>> GetByEMRIdAsync(Guid emrId);
    Task AddAsync(Prescription prescription);
    Task UpdateAsync(Prescription prescription);
}
