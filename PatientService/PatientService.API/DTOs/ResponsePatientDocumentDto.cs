using System;

namespace PatientService.API.DTOs;

public class ResponsePatientDocumentDto
{
    public Guid DocumentID { get; set; }
    public required string PatientID { get; set; }
    public int DocType { get; set; }
    public string? FileURI { get; set; }
    public string? FileName { get; set; }
    public DateTime UploadedDate { get; set; }
}
