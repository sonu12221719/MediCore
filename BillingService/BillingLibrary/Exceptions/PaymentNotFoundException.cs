using System;

namespace BillingLibrary.Exceptions;

public class PaymentNotFoundException : BillingServiceException
{
    public PaymentNotFoundException(Guid id)
        : base($"Payment {id} not found.", 404) { }
}
