using System;
using PatientService.API.DTOs;

namespace PatientService.API.Services;

public interface IPatientService
{
    Task AddPatientAsync(RequestPatientDto dto);
    Task DeletePatientAsync(string patientId);
    Task<IEnumerable<ResponsePatientDto>> GetAllPatientAsync();
    Task<ResponsePatientDto?> GetByIdAsync(string patientId);
    Task UpdateAsync(UpdatePatientDto dto);
}
