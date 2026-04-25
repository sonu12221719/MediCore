using System;

namespace BillingLibrary.Exceptions;

public class ClaimNotFoundException : BillingServiceException
{
    public ClaimNotFoundException(Guid id)
        : base($"Insurance claim {id} not found.", 404) { }
}
