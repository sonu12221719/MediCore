using System;

namespace PatientLibrary.Exceptions;

public class PatientException:Exception
{
    public PatientException(string errMsg):base(errMsg){}
}
