using System;

namespace EmrLibrary.Exceptions;

public class PrescriptionNotFoundException : EmrServiceException
{
    public PrescriptionNotFoundException(Guid id) : base($"Prescription {id} not found.") { }
}
