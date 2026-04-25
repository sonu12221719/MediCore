using System;

namespace Compliance.API.DTOs;

public class AuditResponseDto
{
    public Guid AuditID { get; set; }
    public string AdminID { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
    public string? Findings { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
