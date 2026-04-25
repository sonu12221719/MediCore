using System;

namespace BillingLibrary.Exceptions;

public class DuplicateClaimException : BillingServiceException
{
    public DuplicateClaimException(Guid billId)
        : base($"An active insurance claim already exists for bill {billId}.", 409) { }
}
