using System;

namespace PatientService.API.DTOs;

public class UpdatePatientDto
{
    public required string PatientID { get; set; }
    public DateOnly DOB { get; set; }
    public int Gender { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? InsuranceID { get; set; }
}
