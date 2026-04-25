using System;
using IdentityService.API.DTOs;

namespace IdentityService.API.HttpClientService;

public class PatientServiceClient : IPatientServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PatientServiceClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor=httpContextAccessor;
    }

    public async Task<bool> CreatePatientAsync(string identityUserId, PatientRequestDto dto)
    {
        try
        {
            var payload = new
            {
                PatientID   = identityUserId,
                DOB         = dto.DOB,
                Gender      = dto.Gender,
                Address     = dto.Address,
                Phone       = dto.Phone,
                InsuranceID = dto.InsuranceID
            };

            var response = await _httpClient.PostAsJsonAsync("/api/Patient", payload);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> UpdatePatientAsync(string patientId, PatientRequestDto dto)
    {
        try
        {
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(token))
                throw new Exception("Authorization token is missing.");

            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            
            var payload = new
            {
                PatientID   = patientId,
                DOB         = dto.DOB,
                Gender      = dto.Gender,
                Address     = dto.Address,
                Phone       = dto.Phone,
                InsuranceID = dto.InsuranceID
            };
            var response = await _httpClient.PutAsJsonAsync($"/api/Patient/{patientId}", payload);
            return response.IsSuccessStatusCode;
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
