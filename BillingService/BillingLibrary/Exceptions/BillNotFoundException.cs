using System;

namespace BillingLibrary.Exceptions;

public class BillNotFoundException : BillingServiceException
{
    public BillNotFoundException(Guid id)
        : base($"Bill {id} not found.", 404) { }
}
