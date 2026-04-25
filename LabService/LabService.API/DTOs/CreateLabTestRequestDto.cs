using System;
using LabServiceLibrary.Enums;

namespace LabService.API.DTOs;

public class CreateLabTestRequestDto
{
    public string PatientID { get; set; } = string.Empty;
    public LabTestType Type { get; set; }
}
