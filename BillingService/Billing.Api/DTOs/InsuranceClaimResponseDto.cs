using System;

namespace Billing.Api.DTOs;

public class InsuranceClaimResponseDto
{
    public Guid ClaimID { get; set; }
    public Guid BillID { get; set; }
    public string PatientID { get; set; } = string.Empty;
    public string InsuranceID { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTime Date { get; set; }
}
