using System;

namespace Billing.Api.DTOs;

public class CreateBillRequestDto
{
    public string PatientID { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Description { get; set; }
}
