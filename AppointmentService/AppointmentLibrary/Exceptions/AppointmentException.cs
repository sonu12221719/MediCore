using System;

namespace AppointmentLibrary.Exceptions;

public class AppointmentException:Exception
{
    public AppointmentException(string errMsg):base(errMsg){}
}
