using System;

namespace LabServiceLibrary.Exceptions;

public class LabServiceException : Exception
{
    public LabServiceException(string message) : base(message) { }
}
