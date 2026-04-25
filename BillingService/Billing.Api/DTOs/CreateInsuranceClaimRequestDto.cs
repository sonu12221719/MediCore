using System;

namespace Billing.Api.DTOs;

public class CreateInsuranceClaimRequestDto
{
    public Guid BillID { get; set; }
    public string InsuranceID { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Notes { get; set; }
}
