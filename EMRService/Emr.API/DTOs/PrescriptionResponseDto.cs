using System;

namespace Emr.API.DTOs;

public class PrescriptionResponseDto
{
    public Guid PrescriptionID { get; set; }
    public Guid EMRID { get; set; }
    public string DoctorID { get; set; } = string.Empty;
    public string Medicine { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
