using System;

namespace BillingLibrary.Exceptions;

public class BillingServiceException : Exception
{
    public int StatusCode { get; }

    public BillingServiceException(string message, int statusCode = 400) : base(message)
    {
        StatusCode = statusCode;
    }
}
