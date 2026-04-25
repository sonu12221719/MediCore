using System;

namespace PatientService.API.DTOs;

public class RequestPatientDocumentDto
{
    public int DocType { get; set; }
    public string? FileURI { get; set; }
    public string? FileName { get; set; }
}
