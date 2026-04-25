using System;

namespace BillingLibrary.Exceptions;

public class BillAlreadyPaidException : BillingServiceException
{
    public BillAlreadyPaidException(Guid billId)
        : base($"Bill {billId} is already fully paid.", 400) { }
}

