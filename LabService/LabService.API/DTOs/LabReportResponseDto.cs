using System;

namespace LabService.API.DTOs;

public class LabReportResponseDto
{
    public Guid ReportID { get; set; }
    public Guid TestID { get; set; }
    public string FileURI { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}
