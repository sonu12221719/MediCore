using System;

namespace LabService.API.DTOs;

public class UploadLabReportRequestDto
{
    public IFormFile File { get; set; } = null!;
    public string? Notes { get; set; }
}
