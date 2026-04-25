using System;
using ComplianceLibrary.Enums;

namespace Compliance.API.DTOs;

public class UpdateAuditRequestDto
{
    public AuditStatus Status { get; set; }
    public string? Findings { get; set; }
}
