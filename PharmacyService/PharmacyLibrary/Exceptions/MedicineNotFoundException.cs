using System;

namespace PharmacyLibrary.Exceptions;

public class MedicineNotFoundException : PharmacyServiceException
{
    public MedicineNotFoundException(Guid id) : base($"Medicine {id} not found.", 404) { }
}

