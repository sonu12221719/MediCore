using System;
using BillingLibrary.Enums;

namespace Billing.Api.DTOs;

public class MakePaymentRequestDto
{
    public Guid BillID { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public string? TransactionRef { get; set; }
}
