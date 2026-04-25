using System;

namespace LabServiceLibrary.Exceptions;

public class UnauthorizedLabAccessException : LabServiceException
{
    public UnauthorizedLabAccessException() : base("You are not authorized to access this record.") { }
}
