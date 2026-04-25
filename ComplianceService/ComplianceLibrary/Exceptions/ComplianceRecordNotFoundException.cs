using System;

namespace ComplianceLibrary.Exceptions;

public class ComplianceRecordNotFoundException : ComplianceServiceException
{
    public ComplianceRecordNotFoundException(Guid id)
        : base($"Compliance record {id} not found.", 404) { }
}
