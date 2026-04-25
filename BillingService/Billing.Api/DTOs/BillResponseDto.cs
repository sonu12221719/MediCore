using System;

namespace Billing.Api.DTOs;

public class BillResponseDto
{
    public Guid BillID { get; set; }
    public string PatientID { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal RemainingAmount => Amount - PaidAmount;
    public string? Description { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<PaymentResponseDto> Payments { get; set; } = new List<PaymentResponseDto>();
    public ICollection<InsuranceClaimResponseDto> InsuranceClaims { get; set; } = new List<InsuranceClaimResponseDto>();
}
