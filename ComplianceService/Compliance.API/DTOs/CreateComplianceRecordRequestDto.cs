using System;
using ComplianceLibrary.Enums;

namespace Compliance.API.DTOs;

public class CreateComplianceRecordRequestDto
{
    public string PatientID { get; set; } = string.Empty;
    public ComplianceType Type { get; set; }
    public string? Notes { get; set; }
}
