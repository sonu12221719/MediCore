using System;

namespace EmrLibrary.Exceptions;

public class EMRNotFoundException : EmrServiceException
{
    public EMRNotFoundException(Guid id) : base($"Prescription {id} not found.") { }
}
