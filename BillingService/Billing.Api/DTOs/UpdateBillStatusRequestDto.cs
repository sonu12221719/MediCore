using System;
using BillingLibrary.Enums;

namespace Billing.Api.DTOs;

public class UpdateBillStatusRequestDto
{
    public BillStatus Status { get; set; }
}
