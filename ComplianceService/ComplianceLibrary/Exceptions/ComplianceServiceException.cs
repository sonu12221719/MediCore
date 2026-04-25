using System;

namespace ComplianceLibrary.Exceptions;

public class ComplianceServiceException : Exception
{
    public int StatusCode { get; }

    public ComplianceServiceException(string message, int statusCode = 400) : base(message)
    {
        StatusCode = statusCode;
    }
}