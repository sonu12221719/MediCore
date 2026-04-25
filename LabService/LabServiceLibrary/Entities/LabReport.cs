using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LabServiceLibrary.Enums;

namespace LabServiceLibrary.Entities;

public class LabReport
{
    [Key]
    public Guid ReportID { get; set; }
    public Guid TestID { get; set; }                            // FK → LabTest
    public string FileURI { get; set; } = string.Empty;        // path/url to uploaded file
    public string FileName { get; set; } = string.Empty;       // original file name
    public string? Notes { get; set; }                         // technician observations
    public LabReportStatus Status { get; set; } = LabReportStatus.Uploaded;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    [ForeignKey(nameof(TestID))]
    public LabTest LabTest { get; set; } = null!;
}
