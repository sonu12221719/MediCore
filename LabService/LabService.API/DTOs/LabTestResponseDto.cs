using System;

namespace LabService.API.DTOs;

public class LabTestResponseDto
{
    public Guid TestID { get; set; }
    public string PatientID { get; set; } = string.Empty;
    public string DoctorID { get; set; } = string.Empty;
    public string? TechnicianID { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public LabReportResponseDto? Report { get; set; }
}
