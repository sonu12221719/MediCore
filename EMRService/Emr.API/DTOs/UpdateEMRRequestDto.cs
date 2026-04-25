using System;
using EmrLibrary.Enums;

namespace Emr.API.DTOs;

public class UpdateEMRRequestDto
{
    public string Diagnosis { get; set; } = string.Empty;
    public string TreatmentPlan { get; set; } = string.Empty;
    public EMRStatus Status { get; set; }
}
