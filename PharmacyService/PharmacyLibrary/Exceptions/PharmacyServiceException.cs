using System;

namespace PharmacyLibrary.Exceptions;

public class PharmacyServiceException:Exception
{
    public int StatusCode { get; }
    public PharmacyServiceException(string message, int statusCode = 400) : base(message)
    {
        StatusCode = statusCode;
    }
}
