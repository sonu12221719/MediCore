using System;

namespace PharmacyLibrary.Exceptions;

public class UnauthorizedPharmacyAccessException : PharmacyServiceException
{
    public UnauthorizedPharmacyAccessException() 
        : base("You are not authorized to perform this action.", 403) { }
}
