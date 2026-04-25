using System;

namespace PharmacyLibrary.Exceptions;

public class OutOfStockException : PharmacyServiceException
{
    public OutOfStockException(string name, int available) 
        : base($"Insufficient stock for {name}. Available: {available}.", 400) { }
}
