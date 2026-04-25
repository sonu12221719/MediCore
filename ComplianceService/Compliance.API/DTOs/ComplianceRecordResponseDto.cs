using System;

namespace Compliance.API.DTOs;

public class ComplianceRecordResponseDto
{
    public Guid ComplianceID { get; set; }
    public string PatientID { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Result { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTime Date { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
