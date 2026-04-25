using System;

namespace IdentityService.API.DTOs;

public class PatientRequestDto
{
    public DateOnly DOB { get; set; }
    public int Gender { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? InsuranceID { get; set; }
}
