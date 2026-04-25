using System;
using LabServiceLibrary.Entities;

namespace LabServiceLibrary.Repository;

public interface ILabTestRepository
{
    Task<LabTest?> GetByIdAsync(Guid testId);
    Task<IEnumerable<LabTest>> GetAllAsync();
    Task<IEnumerable<LabTest>> GetByPatientIdAsync(string patientId);
    Task AddAsync(LabTest labTest);
    Task UpdateAsync(LabTest labTest);
}
