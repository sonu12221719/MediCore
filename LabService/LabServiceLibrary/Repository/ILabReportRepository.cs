using System;
using LabServiceLibrary.Entities;

namespace LabServiceLibrary.Repository;

public interface ILabReportRepository
{
    Task<LabReport?> GetByTestIdAsync(Guid testId);
    Task AddAsync(LabReport report);
    Task UpdateAsync(LabReport report);
}
