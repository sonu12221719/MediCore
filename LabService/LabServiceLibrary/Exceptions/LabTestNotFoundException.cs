using System;

namespace LabServiceLibrary.Exceptions;

public class LabTestNotFoundException : LabServiceException
{
    public LabTestNotFoundException(Guid id) : base($"Lab test {id} not found.") { }
}
