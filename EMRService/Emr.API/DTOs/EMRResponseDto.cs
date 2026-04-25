using System;

namespace Emr.API.DTOs;

public class EMRResponseDto
{
    public Guid EMRID { get; set; }
    public string PatientID { get; set; } = string.Empty;
    public string DoctorID { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;
    public string TreatmentPlan { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<PrescriptionResponseDto> Prescriptions { get; set; } = new List<PrescriptionResponseDto>();
    public ICollection<TreatmentLogResponseDto> TreatmentLogs { get; set; } = new List<TreatmentLogResponseDto>();
}
