using System;
using BillingLibrary.Enums;

namespace Billing.Api.DTOs;

public class UpdateClaimStatusRequestDto
{
    public ClaimStatus Status { get; set; }
    public string? Notes { get; set; }
}
