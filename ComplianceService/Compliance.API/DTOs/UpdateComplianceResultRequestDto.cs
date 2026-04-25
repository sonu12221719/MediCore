using System;
using ComplianceLibrary.Enums;

namespace Compliance.API.DTOs;

public class UpdateComplianceResultRequestDto
{
    public ComplianceResult Result { get; set; }
    public string? Notes { get; set; }
}
