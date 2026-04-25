using System;

namespace Billing.Api.DTOs;

public class PaymentResponseDto
{
    public Guid PaymentID { get; set; }
    public Guid BillID { get; set; }
    public decimal Amount { get; set; }
    public string Method { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? TransactionRef { get; set; }
    public DateTime Date { get; set; }
}
