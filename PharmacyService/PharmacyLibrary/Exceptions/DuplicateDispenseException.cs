using System;

namespace PharmacyLibrary.Exceptions;

public class DuplicateDispenseException : PharmacyServiceException
{
    public DuplicateDispenseException(string prescriptionId) 
        : base($"Prescription {prescriptionId} has already been dispensed.", 409) { }
}
