using System;
using EmrLibrary.Entities;

namespace EmrLibrary.Repository;

public interface IEMRRepository
{
    Task<EMR?> GetByIdAsync(Guid emrId);
    Task<IEnumerable<EMR>> GetByPatientIdAsync(string patientId);
    Task<IEnumerable<EMR>> GetByDoctorIdAsync(string doctorId);
    Task AddAsync(EMR emr);
    Task UpdateAsync(EMR emr);
}
