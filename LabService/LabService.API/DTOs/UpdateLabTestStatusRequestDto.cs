using System;
using LabServiceLibrary.Enums;

namespace LabService.API.DTOs;

public class UpdateLabTestStatusRequestDto
{
    public LabTestStatus Status { get; set; }
}
