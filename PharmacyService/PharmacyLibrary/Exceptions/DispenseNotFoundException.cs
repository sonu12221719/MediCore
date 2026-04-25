using System;

namespace PharmacyLibrary.Exceptions;

public class DispenseNotFoundException : PharmacyServiceException
{
    public DispenseNotFoundException(Guid id) : base($"Dispense record {id} not found.", 404) { }
}
