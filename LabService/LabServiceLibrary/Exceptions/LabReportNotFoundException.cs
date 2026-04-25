using System;

namespace LabServiceLibrary.Exceptions;

public class LabReportNotFoundException : LabServiceException
{
    public LabReportNotFoundException(Guid testId) : base($"Report for test {testId} not found.") { }
}
