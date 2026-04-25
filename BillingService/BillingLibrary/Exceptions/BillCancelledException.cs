using System;

namespace BillingLibrary.Exceptions;

public class BillCancelledException : BillingServiceException
{
    public BillCancelledException(Guid billId)
        : base($"Bill {billId} is cancelled.", 400) { }
}
