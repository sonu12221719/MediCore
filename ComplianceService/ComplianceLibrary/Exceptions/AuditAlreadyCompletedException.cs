using System;

namespace ComplianceLibrary.Exceptions;

public class AuditAlreadyCompletedException : ComplianceServiceException
{
    public AuditAlreadyCompletedException(Guid id)
        : base($"Audit {id} is already completed and cannot be modified.", 400) { }
}
