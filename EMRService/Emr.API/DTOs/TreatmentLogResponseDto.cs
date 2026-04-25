using System;

namespace Emr.API.DTOs;

public class TreatmentLogResponseDto
{
    public Guid LogID { get; set; }
    public Guid EMRID { get; set; }
    public string NurseID { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}
