using System;
using EmrLibrary.Entities;

namespace EmrLibrary.Repository;

public interface ITreatmentLogRepository
{
    Task<TreatmentLog?> GetByIdAsync(Guid logId);
    Task<IEnumerable<TreatmentLog>> GetByEMRIdAsync(Guid emrId);
    Task AddAsync(TreatmentLog log);
    Task UpdateAsync(TreatmentLog log);
}
