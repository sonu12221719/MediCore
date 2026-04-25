using System;

namespace EmrLibrary.Exceptions;

public class UnauthorizedEMRAccessException : EmrServiceException
{
    public UnauthorizedEMRAccessException() : base("You are not authorized to access this record.") { }
}
