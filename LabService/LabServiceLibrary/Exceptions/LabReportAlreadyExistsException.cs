using System;

namespace LabServiceLibrary.Exceptions;

public class LabReportAlreadyExistsException : LabServiceException
{
    public LabReportAlreadyExistsException(Guid testId) : base($"Report for test {testId} already exists.") { }
}
