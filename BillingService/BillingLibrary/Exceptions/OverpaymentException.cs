using System;

namespace BillingLibrary.Exceptions;

public class OverpaymentException : BillingServiceException
{
    public OverpaymentException(decimal remaining)
        : base($"Payment exceeds remaining bill amount. Remaining: {remaining:C}.", 400) { }
}
