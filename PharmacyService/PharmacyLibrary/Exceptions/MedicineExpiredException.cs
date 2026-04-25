using System;

namespace PharmacyLibrary.Exceptions;

public class MedicineExpiredException : PharmacyServiceException
{
    public MedicineExpiredException(string name) 
        : base($"{name} is expired and cannot be dispensed.", 400) { }
}
