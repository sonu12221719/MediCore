using System;
using IdentityService.API.DTOs;

namespace IdentityService.API.HttpClientService;

public interface IPatientServiceClient
{
    Task<bool> CreatePatientAsync(string identityUserId, PatientRequestDto dto);
    Task<bool> UpdatePatientAsync(string patientId, PatientRequestDto dto);
}   
