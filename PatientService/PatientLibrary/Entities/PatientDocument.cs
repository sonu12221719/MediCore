using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PatientLibrary.Enums;

namespace PatientLibrary.Entities;

public class PatientDocument
{
    [Key]
    [Column(TypeName ="varchar(4)")]
    public Guid DocumentID { get; set; }
    public string PatientID { get; set; } = null!;
    public DocTypeOption DocType { get; set; }
    public string FileURI { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public DateTime UploadedDate { get; set; }

    public Patient Patient { get; set; } = null!;
}
