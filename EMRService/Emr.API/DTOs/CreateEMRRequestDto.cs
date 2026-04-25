using System;

namespace Emr.API.DTOs;

public class CreateEMRRequestDto
{
    public string PatientID { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;
    public string TreatmentPlan { get; set; } = string.Empty;
}
