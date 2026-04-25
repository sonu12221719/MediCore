using System;

namespace Emr.API.DTOs;

public class AddPrescriptionRequestDto
{
    public string Medicine { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
}
