using System;

namespace ComplianceLibrary.Exceptions;

public class AuditNotFoundException : ComplianceServiceException
{
    public AuditNotFoundException(Guid id)
        : base($"Audit {id} not found.", 404) { }
}
