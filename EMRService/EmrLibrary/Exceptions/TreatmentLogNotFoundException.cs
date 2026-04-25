using System;

namespace EmrLibrary.Exceptions;

public class TreatmentLogNotFoundException : EmrServiceException
{
    public TreatmentLogNotFoundException(Guid id) : base($"Treatment log {id} not found.") { }
}
